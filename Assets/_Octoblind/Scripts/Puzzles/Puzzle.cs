using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public int index;
    public bool[] steps;
    public bool completed;
    public List<GameObject> PuzzlePieces;
    private void Start()
    {
        PuzzlePieces = new List<GameObject>();
    }
    public void AchiveStep(int step, bool state)
    {
        this.steps[step] = state;
        MiddleStep();
        if(state)this.completed = CheckForCompletion();
    }
    private bool CheckForCompletion()
    {
        foreach(bool b in this.steps)
        {
            if (!b) return false;
        }
        return true;
    }
    public virtual void MiddleStep() { }
    public void AddPiece(GameObject toAdd)
    {
        PuzzlePieces.Add(toAdd);
    }
    public void RemovePiece(GameObject toRemove)
    {
        PuzzlePieces.Remove(toRemove);
    }
}
