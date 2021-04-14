using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePuzzleCollider : MonoBehaviour
{
    [SerializeField]
    private int step;
    [SerializeField]
    private Puzzle p;
    private void Start()
    {
        if(GetComponentInParent<Puzzle>() != null)
        {
            p = GetComponentInParent<Puzzle>();
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
            p.AddPiece(other.transform.parent.gameObject);
            p.AchiveStep(step, true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Trigger"))
        {
            p.RemovePiece(other.transform.parent.gameObject);
            p.AchiveStep(step, false);
        }
    }
}
