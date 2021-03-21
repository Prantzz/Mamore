using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public int behaviour;
    public int Power;
    public int Strengh;
    public float Frequency;
    private Light LightToChange;
    void Start()
    {
        LightToChange = GetComponentInChildren<Light>();
    }

    void Update()
    {
        LightToChange.intensity = Strengh*Mathf.Sin((Time.frameCount)*Frequency)+Power;
    }
}
