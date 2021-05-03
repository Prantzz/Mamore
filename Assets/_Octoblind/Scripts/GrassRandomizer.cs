using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassRandomizer : MonoBehaviour
{
    void Start()
    {
        this.transform.eulerAngles = Vector3.up * Random.Range(0, 360);
    }

}
