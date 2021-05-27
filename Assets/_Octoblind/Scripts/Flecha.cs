using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    [SerializeField]
    LayerMask desiredBreakLayerMask;

    

    private void OnCollisionEnter(Collision collision)
    {
        if(desiredBreakLayerMask == (desiredBreakLayerMask | (1 << collision.gameObject.layer))) 
        {
            Destroy(gameObject);
        }
    }
}
