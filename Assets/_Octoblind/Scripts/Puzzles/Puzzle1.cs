using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : Puzzle
{
    //Puzzle da escada

    [SerializeField] private Transform finalPosDegrau1;
    [SerializeField] private Transform finalPosDegrau2;
    int i = 0;
    bool locker1 = false;
    bool locker2 = false;

    public override void MiddleStep() 
    {
        Transform Degrau0 = transform.GetChild(0);
        Transform Degrau1 = transform.GetChild(1);
        
        //Travar degrau 1
        if (steps[0] && steps[1] && !locker1)
        {
            AjustarDegrau(finalPosDegrau1.position, PuzzlePieces[i]);
            Degrau0.GetChild(0).GetComponent<SimplePuzzleCollider>().canCollide = false;
            Degrau0.GetChild(1).GetComponent<SimplePuzzleCollider>().canCollide = false;
            locker1 = true;
        } 
       
        if(steps[2] && steps[3] && !locker2)
        {
            AjustarDegrau(finalPosDegrau2.position, PuzzlePieces[i]);
            Degrau1.GetChild(0).GetComponent<SimplePuzzleCollider>().canCollide = false;
            Degrau1.GetChild(1).GetComponent<SimplePuzzleCollider>().canCollide = false;
            locker2 = true;
        }

    }

    public void AjustarDegrau(Vector3 pos, GameObject degrau)
    {
        GameObject Degrau0 = transform.GetChild(0).gameObject;
        GameObject Degrau1 = transform.GetChild(1).gameObject;
        i++;

        degrau.transform.SetParent(transform);
        degrau.transform.position = pos;
        degrau.transform.eulerAngles = new Vector3(0, 90, 0);
        degrau.GetComponent<objectController>().enabled = false;
        degrau.GetComponent<BoxCollider>().enabled = false;
        degrau.transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
        degrau.transform.GetChild(2).GetComponent<BoxCollider>().enabled = false;
        degrau.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        if (!transform.GetChild(1).gameObject.activeSelf)
        {
            Degrau1.gameObject.SetActive(true);
           // Debug.Log("Ativou degrau 2");
        }
        if (!transform.GetChild(0).gameObject.activeSelf)
        {
            Degrau0.gameObject.SetActive(true);
           // Debug.Log("Ativou degrau 1");
        }

        if (Degrau0.GetComponent<OnTriggerEnterEvent>()?.enabled == true)
            Degrau0.GetComponent<OnTriggerEnterEvent>().ResetTrigger();
        if (Degrau1.GetComponent<OnTriggerEnterEvent>()?.enabled == true)
            Degrau1.GetComponent<OnTriggerEnterEvent>().ResetTrigger();
        Debug.Log("yoyuu");

    }

    public void DesajustarDegrau()
    {
        if(!(CheckStep(4) && CheckStep(5)))
        {
            PuzzlePieces[0].GetComponent<objectController>().enabled = true;
            PuzzlePieces[0].GetComponent<BoxCollider>().enabled = true;
            PuzzlePieces[0].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            PuzzlePieces[0].GetComponent<Rigidbody>().isKinematic = false;
            PuzzlePieces[0].transform.GetChild(1).GetComponent<BoxCollider>().enabled = true;
            PuzzlePieces[0].transform.GetChild(2).GetComponent<BoxCollider>().enabled = true;
            transform.GetChild(0).GetChild(0).GetComponent<SimplePuzzleCollider>().canCollide = true;
            transform.GetChild(0).GetChild(1).GetComponent<SimplePuzzleCollider>().canCollide = true;
            locker1 = false;
        }

        if (!(CheckStep(6) && CheckStep(7)) && PuzzlePieces[1])
        {
            PuzzlePieces[1].GetComponent<objectController>().enabled = true;
            PuzzlePieces[1].GetComponent<BoxCollider>().enabled = true;
            PuzzlePieces[1].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            PuzzlePieces[1].GetComponent<Rigidbody>().isKinematic = false;
            PuzzlePieces[1].transform.GetChild(1).GetComponent<BoxCollider>().enabled = true;
            PuzzlePieces[1].transform.GetChild(2).GetComponent<BoxCollider>().enabled = true;
            transform.GetChild(1).GetChild(0).GetComponent<SimplePuzzleCollider>().canCollide = true;
            transform.GetChild(1).GetChild(1).GetComponent<SimplePuzzleCollider>().canCollide = true;
            locker2 = false;
        }
    }

}
