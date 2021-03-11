using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInstanceComponent : MonoBehaviour
{
    public AudioManager AU;
    public AudioClip AC;
    private AudioSource OwnAS;
    public void OnEnable()
    {
        OwnAS = GetComponent<AudioSource>();
        OwnAS.clip = AC;
        OwnAS.Play();
    }
    public void Start()
    {
        OwnAS = GetComponent<AudioSource>();
        OwnAS.clip = AC;
        OwnAS.Play();
    }
    private void Update()
    {
        if (!OwnAS.isPlaying)
        {
            AU.Comeback(this.gameObject);
        }
    }
}
