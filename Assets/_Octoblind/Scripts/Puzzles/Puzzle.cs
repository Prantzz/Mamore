using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public int index;
    public bool[] steps;
    public bool completed;
    public PuzzlePart[] parts;
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
    private void MiddleStep() { }
}
