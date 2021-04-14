using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : Puzzle
{
    public Transform finalPosTabua1;
    public Transform finalPosTabua2;
    public override void MiddleStep()
    {
        //Travar taboa 1
        if (steps[0] && steps[1])
        {
            AjustarBatente(finalPosTabua1.position,PuzzlePieces[0]);
        }
        //Travar taboa 1
        if (steps[2] && steps[3])
        {
            AjustarBatente(finalPosTabua2.position, PuzzlePieces[1]);
        }
    }
    public void AjustarBatente(Vector3 pos, GameObject batente)
    {
        batente.transform.position = pos;
        batente.transform.rotation = new Quaternion(0, 0, 0, 0);
        batente.transform.SetParent(this.gameObject.transform);
        batente.GetComponent<objectController>().enabled = false;
        batente.GetComponent<BoxCollider>().enabled = false;
        batente.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
