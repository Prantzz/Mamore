using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePuzzleCollider : MonoBehaviour
{
    private PuzzlePart p;
    private void Start()
    {
        if(GetComponentInParent<PuzzlePart>() != null)
        {
            p = GetComponentInParent<PuzzlePart>();
        }
        else
        {
            Debug.LogError("Esse collider não tem um puzzle part como parent!",this);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trigger"))
        {
            p.ModifyPuzzle(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Trigger"))
        {
            p.ModifyPuzzle(other.gameObject);
        }
    }
}
