using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public int index;
    public bool[] steps;
    public bool completed;
    public GameObject[] parts;
    public void AchiveStep(int step)
    {
        this.steps[step] = true;
        this.completed = CheckForCompletion();
    }
    private bool CheckForCompletion()
    {
        foreach(bool b in this.steps)
        {
            if (!b) return false;
        }
        return true;
    }
}
