using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle5 : Puzzle
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
            CC.AddQuestMarker(CC.quest2);
            QCL.changeTextAndActive(CC.quest2.description, true);
            PuzzlePieces[1].SetActive(true);
        }
        if (steps[1] && steps[0] && !steps[2])
        {
            this.AchiveStep(2, true);
            GameObject x = PuzzlePieces[1];
            QCL.changeTextAndActive(CC.quest2.description, false);
            CC.RemoveQuestMarker(CC.quest1_1);
            RemovePiece(PuzzlePieces[1]);

            x.SetActive(false);
        }
        if (steps[0] && steps[1] && steps[2])
        {
            PuzzlePieces[0].SetActive(true);
            CC.RemoveQuestMarker(CC.quest2);
            CC.AddQuestMarker(CC.quest5);
            QCL.changeTextAndActive(CC.quest5.description, true);
            
        }

    }
}
