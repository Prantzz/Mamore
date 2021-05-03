using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle4 : Puzzle
{
    public override void MiddleStep()
    {
        if (steps[0] && steps[1] && steps[2]) PuzzlePieces[0].SetActive(true);
        if(steps[1] && steps[0] && !steps[2])
        {
            this.AchiveStep(2, true);
            GameObject x = PuzzlePieces[1];
            RemovePiece(PuzzlePieces[1]);
            Destroy(x);
        }
    }

}
