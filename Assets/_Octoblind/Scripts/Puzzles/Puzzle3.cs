using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle3 : Puzzle
{
    //Puzzle do trilho

    // Esse script era originalmente nomeado "puzzle 1", mas só pra fins de organização coloquei como puzzle 3,
    // porque o puzzle de trilho na teoria é o último puzzle

    public Transform finalPosTabua1;
    public Transform finalPosTabua2;
    public override void MiddleStep()
    {
        //Travar tabua 1
        if (steps[0] && steps[1])
        {
            AjustarBatente(finalPosTabua1.position, PuzzlePieces[0]);
            this.transform.GetChild(0).GetChild(0).GetComponent<SimplePuzzleCollider>().canCollide = false;
            this.transform.GetChild(0).GetChild(1).GetComponent<SimplePuzzleCollider>().canCollide = false;
        }
        //Travar tabua 2
        if (steps[2] && steps[3])
        {
            AjustarBatente(finalPosTabua2.position, PuzzlePieces[1]);
            this.transform.GetChild(1).GetChild(0).GetComponent<SimplePuzzleCollider>().canCollide = false;
            this.transform.GetChild(1).GetChild(1).GetComponent<SimplePuzzleCollider>().canCollide = false;
        }
    }
    public void AjustarBatente(Vector3 pos, GameObject batente)
    {               
        batente.transform.SetParent(this.gameObject.transform);
        batente.transform.position = pos;
        batente.transform.rotation = new Quaternion(0, 0, 0, 0);
        batente.GetComponent<objectController>().enabled = false;
        batente.GetComponent<BoxCollider>().enabled = false;
        batente.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
