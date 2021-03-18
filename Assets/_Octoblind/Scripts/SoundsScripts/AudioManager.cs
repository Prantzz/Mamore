using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public bool playable;
    public GameObject AudioInstancePrefab;
    public List<AudioClip> Ambients;
    public Stack<GameObject> AudioInstances;
    public List<AudioGroup> AudioGroupes;
    private AudioSource AmbientePlayer;
    void Start()
    {
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
                AmbientePlayer.clip = Ambients[0];
                break;
        }
        AmbientePlayer.Play();
    }
    public void PullSound (Vector3 pos, int type, int sound)
    {
        
        if (playable)
        {
            //Caso a lista esteja vazia
            if (AudioInstances.Count <= 0)
            {
                GameObject toInstantiate = Instantiate(AudioInstancePrefab, pos, Quaternion.identity);
                toInstantiate.GetComponent<AudioInstanceComponent>().AU = this;
                toInstantiate.GetComponent<AudioInstanceComponent>().AC = AudioGroupes[type].Sounds[sound];
            }
            //Caso a lista tenha objetos
            else
            {
                GameObject Popped = AudioInstances.Pop();
                Popped.transform.parent = null;
                Popped.transform.position = pos;
                Popped.GetComponent<AudioInstanceComponent>().AC = AudioGroupes[type].Sounds[sound];
                Popped.SetActive(true);
            }
        }
        
    }
    //Volta tudo para lista quando terminar de trocar.
    public void Comeback(GameObject toSave)
    {
        AudioInstances.Push(toSave);
        toSave.transform.SetParent(this.transform);
        toSave.transform.TransformPoint(Vector3.zero);
        toSave.GetComponent<AudioInstanceComponent>().AC = null;
        toSave.SetActive(false);
    }
    public void PauseSound()
    {
        AmbientePlayer.volume = 0.15f;
    }
    public void ResumeSound()
    {
        AmbientePlayer.volume = 0.50f;
    }

    private void xxx()
    {

    }
}
