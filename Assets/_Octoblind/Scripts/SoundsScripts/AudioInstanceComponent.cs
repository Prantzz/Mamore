using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInstanceComponent : MonoBehaviour
{
    public bool Paused;
    public AudioManager AU;
    public AudioClip AC;
    private AudioSource OwnAS;
    public void OnEnable()
    {
        Paused = false;
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
        if (!OwnAS.isPlaying && !Paused)
        {
            AU.Comeback(this.gameObject);
        }
    }
}
