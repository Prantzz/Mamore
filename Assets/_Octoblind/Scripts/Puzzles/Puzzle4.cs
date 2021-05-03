using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle4 : Puzzle
{
    public override void MiddleStep()
    {
        if (steps[0] && steps[1] && steps[2]) PuzzlePieces[0].SetActive(true);
        if(steps[1] && steps[0])
        {
            Debug.Log(PuzzlePieces[1].name);
            Debug.Log(PuzzlePieces[2].name);
            if (PuzzlePieces[1].name == PuzzlePieces[2].name)
            {
                Debug.Log("ehheheheh");
                this.AchiveStep(2, true);
                Destroy(PuzzlePieces[2]);
            }
        }
    }

}
