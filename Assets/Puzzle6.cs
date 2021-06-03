using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle6 : Puzzle
{
    public CompassController CC;
    public QuestCanvasLogic QCL;
    private void Start()
    {
        PuzzlePieces[1].SetActive(false);
    }
    public override void MiddleStep()
    {
        if (steps[0] && !steps[1] && !steps[2])
        {
            CC.AddQuestMarker(CC.quest3);
            QCL.changeTextAndActive(CC.quest3.description, true);
        }
        if (steps[1] && steps[0] && !steps[2])
        {
            this.AchiveStep(2, true);
            //GameObject x = PuzzlePieces[1];
            QCL.changeTextAndActive(CC.quest3.description, false);
            //RemovePiece(PuzzlePieces[1]);
            //Destroy(x);
        }
        if (steps[0] && steps[1] && steps[2])
        {
            PuzzlePieces[0].SetActive(true);
            PuzzlePieces[1].SetActive(true);
        }
    }
}
