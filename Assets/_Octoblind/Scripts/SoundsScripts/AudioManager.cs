using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("HARD CODED 4 e 5!!!")]
    public bool playable;
    public GameObject AudioInstancePrefab;
    public List<AudioClip> Ambients;
    public Stack<GameObject> AudioInstances;
    public List<GameObject> AudioInstancesPlaying;
    public List<AudioGroup> AudioGroupes;
    private AudioSource AmbientePlayer;
    private PlayerSoundController PSC;
    [HideInInspector]
    public ProceduralSound PS;
    void Start()
    {

        //---SetUp do Procedural---
        PS = GetComponent<ProceduralSound>();
        PS.AU = this;
        PS.SetUp();
        PS.enabled = false;
        // -------------------------


        playable = true;
        AudioInstances = new Stack<GameObject>();
        PullSound(Camera.main.transform.position, 1, 2);
        AmbientePlayer = GetComponent<AudioSource>();
        SceneManager.activeSceneChanged += AmbientForScene;
    }
    void AmbientForScene(Scene cur, Scene next)
    {
        switch (next.buildIndex)
        {
            case (1):
                //Aqui virá a música do menu inicial
                break;
            case (2):
                //Ativa as coisas necessárias quando o jogador entra no main game.
                AmbientePlayer.clip = Ambients[0];
                PSC = GameObject.Find("Player").GetComponent<PlayerSoundController>();
                PS.enabled = true;
                PS.PlayerPos = PSC.gameObject.transform;
                PS.InvokeRepeating("InvokeSound", 5, 60);
                break;
        }
        AmbientePlayer.Play();
    }
    public GameObject PullSound (Vector3 pos, int type, int sound)
    {        
        if (playable)
        {
            //Caso a lista esteja vazia
            if (AudioInstances.Count <= 0)
            {
                GameObject toInstantiate = Instantiate(AudioInstancePrefab, pos, Quaternion.identity);
                toInstantiate.GetComponent<AudioInstanceComponent>().AU = this;
                toInstantiate.GetComponent<AudioInstanceComponent>().AC = AudioGroupes[type].Sounds[sound];
                AudioInstancesPlaying.Add(toInstantiate);
                return toInstantiate;
            }
            //Caso a lista tenha objetos
            else
            {
                GameObject Popped = AudioInstances.Pop();
                AudioInstancesPlaying.Add(Popped);
                Popped.transform.parent = null;
                Popped.transform.position = pos;
                Popped.GetComponent<AudioInstanceComponent>().AC = AudioGroupes[type].Sounds[sound];
                Popped.SetActive(true);                
                return Popped;
            }
        }
        else return null;        
    }
    //Volta tudo para lista quando terminar de trocar.
    public void Comeback(GameObject toSave)
    {
        AudioInstancesPlaying.Remove(toSave);
        AudioInstances.Push(toSave);
        toSave.transform.SetParent(this.transform);
        toSave.transform.TransformPoint(Vector3.zero);
        toSave.GetComponent<AudioInstanceComponent>().AC = null;
        toSave.SetActive(false);
    }
    public void PauseSound()
    {
        if (AudioInstancesPlaying.Count > 0)
        {
            foreach (GameObject G in AudioInstancesPlaying)
            {
                if (G != null)
                {
                    G.GetComponent<AudioInstanceComponent>().Paused = true;
                    G.GetComponent<AudioSource>().Pause();
                }
            }
        }        
        PSC.WalkingAudio.Pause();
        AmbientePlayer.volume = 0.15f;
    }
    public void ResumeSound()
    {
        foreach (GameObject G in AudioInstancesPlaying)
        {
            if (G != null)
            {
                G.GetComponent<AudioSource>().Play();
                G.GetComponent<AudioInstanceComponent>().Paused = false;
            }
        }
        if (!PSC.WalkingAudio.isPlaying) PSC.WalkingAudio.Play();
        AmbientePlayer.volume = 0.50f;
    }
}
