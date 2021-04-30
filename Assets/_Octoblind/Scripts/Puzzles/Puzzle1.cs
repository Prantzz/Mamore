using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : Puzzle
{
    //Puzzle da escada

    [SerializeField] private Transform finalPosDegrau1;
    [SerializeField] private Transform finalPosDegrau2;


    public override void MiddleStep() 
    {
        //Travar degrau 1
        if (steps[0] && steps[1])
        {
            AjustarDegrau(finalPosDegrau1.position, PuzzlePieces[0]);
            this.transform.GetChild(0).GetChild(0).GetComponent<SimplePuzzleCollider>().canCollide = false;
            this.transform.GetChild(0).GetChild(1).GetComponent<SimplePuzzleCollider>().canCollide = false;
        }
        
        if(steps[2] && steps[3])
        {
            AjustarDegrau(finalPosDegrau2.position, PuzzlePieces[1]);
            this.transform.GetChild(1).GetChild(0).GetComponent<SimplePuzzleCollider>().canCollide = false;
            this.transform.GetChild(1).GetChild(1).GetComponent<SimplePuzzleCollider>().canCollide = false;
        }
    }

    public void AjustarDegrau(Vector3 pos, GameObject degrau)
    {
        degrau.transform.SetParent(transform);
        degrau.transform.position = pos;
        degrau.transform.eulerAngles = new Vector3(0, 90, 0);
        degrau.GetComponent<objectController>().enabled = false;
        degrau.GetComponent<BoxCollider>().enabled = false;
        degrau.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    /*public void DesajustarDegrau(int pieceIndex)
    {
        PuzzlePieces[pieceIndex].GetComponent<objectController>().enabled = true;
        PuzzlePieces[pieceIndex].GetComponent<BoxCollider>().enabled = true;
        PuzzlePieces[pieceIndex].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }*/

}
