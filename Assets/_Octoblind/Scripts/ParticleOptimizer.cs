using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOptimizer : MonoBehaviour
{
    public bool StartOn;
    private ParticleSystem.EmissionModule ownPSEmit;
    void Start()
    {
        ownPSEmit = GetComponent<ParticleSystem>().emission;
        ownPSEmit.enabled = StartOn;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))this.ownPSEmit.enabled = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) this.ownPSEmit.enabled = false;
    }
}
