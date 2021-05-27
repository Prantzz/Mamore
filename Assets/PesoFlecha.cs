using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PesoFlecha : MonoBehaviour
{
    private Rigidbody ownRB;

    private void Start()
    {
        ownRB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(ownRB.velocity);
    }
}
