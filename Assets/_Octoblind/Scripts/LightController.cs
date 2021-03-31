using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light[] LightToChange;
    private float[] RandomSeeds;
    public LightControllerSettings[] LightSettings;
    public int behaviour;


    void Start()
    {
        if (LightSettings.Length != LightToChange.Length) Debug.LogError("O objeto " + this.name + " tem uma quantidade diferente de luzes e settings para elas.");
        RandomSeeds = new float[LightToChange.Length];
        for(int i = 0; i < RandomSeeds.Length; i++)
        {
            LightSettings[i].name = LightToChange[i].name;
            RandomSeeds[i] = Random.Range(0, 100);
        }
    }

    void Update()
    {
        switch (behaviour)
        {
            //fogueira
            case (0):
                for(int i = 0; i < LightToChange.Length; i++)
                {
                    LightToChange[i].intensity = LightSettings[i].Strengh * Mathf.Sin(((Time.frameCount) * LightSettings[i].Frequency) + RandomSeeds[i]) + LightSettings[i].Power;
                }
                break;

        }
        
    }
}
