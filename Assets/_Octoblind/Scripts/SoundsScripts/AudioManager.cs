using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject AudioInstancePrefab;
    public List<AudioClip> Ambients;
    public Stack<GameObject> AudioInstances;
    public List<AudioGroup> AudioGroupes;
    void Start()
    {
        AudioInstances = new Stack<GameObject>();
    }
    public void PullSound (Vector3 pos, int type, int sound)
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
    public void Comeback(GameObject toSave)
    {
        AudioInstances.Push(toSave);
        toSave.transform.SetParent(this.transform);
        toSave.transform.TransformPoint(Vector3.zero);
        toSave.GetComponent<AudioInstanceComponent>().AC = null;
        toSave.SetActive(false);
    }

}
