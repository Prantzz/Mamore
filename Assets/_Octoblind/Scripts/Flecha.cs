using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    [SerializeField]
    LayerMask desiredBreakLayerMask;
    [SerializeField]
    LayerMask desiredInteractionLayerMask;

    Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
        if (desiredBreakLayerMask == (desiredBreakLayerMask | (1 << collision.gameObject.layer)))
        {
            Destroy(gameObject);
        }
        if (desiredInteractionLayerMask == (desiredInteractionLayerMask | (1 << collision.gameObject.layer)))
        {
            if (collision.transform.parent != null)
            { rb = collision.transform.parent.GetComponent<Rigidbody>(); }
            else
            { rb = collision.transform.GetComponent<Rigidbody>(); }
            rb.isKinematic = false;
        }
    }
}
