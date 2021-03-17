using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassCanvas : MonoBehaviour
{
    public GameObject canvasbg, canvas;
    void Update()
    {
        if (GameGlobeData.isCompassCollected)
        {
            canvasbg.SetActive(true);
            canvas.SetActive(true);
        }
        else 
        {
            canvasbg.SetActive(false);
            canvas.SetActive(false);
        }
    }
}
