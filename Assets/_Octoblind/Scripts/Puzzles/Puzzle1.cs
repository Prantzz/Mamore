using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : Puzzle
{
    //Puzzle da escada

    [SerializeField] private Transform finalPosDegrau1;
    [SerializeField] private Transform finalPosDegrau2;
    private Transform Degrau0;
    private Transform Degrau1;
    private int a = 0;
    bool locker1 = false;
    bool locker2 = false;

    private void Start()
    {
        Degrau0 = transform.GetChild(0);
        Degrau1 = transform.GetChild(1);
    }

    //Chamado pelo AchieveStep
    public override void MiddleStep() 
    {
       
        //Travar degrau 1
        if (steps[0] && steps[1] && !locker1)
        {
            Degrau0.GetComponent<OnTriggerEnterEvent>().AlternateTrigger(true);
            if (!Degrau1.gameObject.activeSelf)
                Degrau1.gameObject.SetActive(true);
            ArrayPuzzlePieces[0].GetComponent<objectController>().hasInteracted = false;
            AjustarDegrau(finalPosDegrau1.position, ArrayPuzzlePieces[0]);
            Degrau0.GetChild(0).GetComponent<SimplePuzzleCollider>().canCollide = false;
            Degrau0.GetChild(1).GetComponent<SimplePuzzleCollider>().canCollide = false;
            locker1 = true;
            
        }

        //Travar degrau 2
        if (steps[2] && steps[3] && !locker2)
        {
            Degrau1.GetComponent<OnTriggerEnterEvent>().AlternateTrigger(true);
            if (!Degrau0.gameObject.activeSelf)
                Degrau0.gameObject.SetActive(true);

            ArrayPuzzlePieces[1].GetComponent<objectController>().hasInteracted = false;
            AjustarDegrau(finalPosDegrau2.position, ArrayPuzzlePieces[1]);
            Degrau1.GetChild(0).GetComponent<SimplePuzzleCollider>().canCollide = false;
            Degrau1.GetChild(1).GetComponent<SimplePuzzleCollider>().canCollide = false;
            locker2 = true;
        }

    }

 

    public void AjustarDegrau(Vector3 pos, GameObject degrau)
    {

        degrau.transform.SetParent(transform);
        degrau.transform.position = pos;
        degrau.transform.eulerAngles = new Vector3(0, 90, 0);
        degrau.GetComponent<objectController>().enabled = false;
        degrau.GetComponent<BoxCollider>().enabled = false;
        degrau.transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
        degrau.transform.GetChild(2).GetComponent<BoxCollider>().enabled = false;
        degrau.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
     

    }

    public override void DesajustarParte(GameObject tabua)
    {

        if(ArrayPuzzlePieces.Length > 0)
        {
            if (Array.IndexOf(ArrayPuzzlePieces, tabua) == 0)
            {
                if ((!CheckStep(4) || !CheckStep(5)))
                {
                    //Debug.Log($"desajustou degrau1: {tabua}");
                    tabua.transform.SetParent(null);
                    tabua.GetComponent<objectController>().enabled = true;
                    tabua.GetComponent<BoxCollider>().enabled = true;
                    tabua.transform.GetChild(1).GetComponent<BoxCollider>().enabled = true;
                    tabua.transform.GetChild(2).GetComponent<BoxCollider>().enabled = true;
                    tabua.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    tabua.GetComponent<Rigidbody>().isKinematic = false;
                    tabua.transform.eulerAngles = new Vector3(0, 45, 0);
                    tabua.GetComponent<objectController>().isSelected = false;
                    tabua.GetComponent<objectController>().hasInteracted = false;
                    Degrau0.GetChild(0).GetComponent<SimplePuzzleCollider>().canCollide = true;
                    Degrau0.GetChild(1).GetComponent<SimplePuzzleCollider>().canCollide = true;
                    locker1 = false;
                    Degrau0.GetComponent<OnTriggerEnterEvent>().AlternateTrigger(false);
                    if (!Degrau1.gameObject.activeSelf)
                        Degrau1.gameObject.SetActive(true);

                    AchiveStep(0, false);
                    AchiveStep(1, false);
                    AchiveStep(4, false);
                    AchiveStep(5, false);
                    
                    if (ArrayPuzzlePieces[0] != null)
                        ArrayPuzzlePieces[0] = null;

                }
            }
            else if (Array.IndexOf(ArrayPuzzlePieces, tabua) == 1)
            {
                if ((!CheckStep(6) || !CheckStep(7)))
                {
                    //Debug.Log($"desajustou degrau2: {tabua}");
                    tabua.transform.SetParent(null);
                    tabua.GetComponent<objectController>().enabled = true;
                    tabua.GetComponent<BoxCollider>().enabled = true;
                    tabua.transform.GetChild(1).GetComponent<BoxCollider>().enabled = true;
                    tabua.transform.GetChild(2).GetComponent<BoxCollider>().enabled = true;
                    tabua.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    tabua.GetComponent<Rigidbody>().isKinematic = false;
                    tabua.transform.eulerAngles = new Vector3(0, 45, 0);
                    tabua.GetComponent<objectController>().isSelected = false;
                    tabua.GetComponent<objectController>().hasInteracted = false;
                    Degrau1.GetChild(0).GetComponent<SimplePuzzleCollider>().canCollide = true;
                    Degrau1.GetChild(1).GetComponent<SimplePuzzleCollider>().canCollide = true;
                    locker2 = false;
                    Degrau1.GetComponent<OnTriggerEnterEvent>().AlternateTrigger(false);
                    if (!Degrau0.gameObject.activeSelf)
                        Degrau0.gameObject.SetActive(true);

                    AchiveStep(2, false);
                    AchiveStep(3, false);
                    AchiveStep(6, false);
                    AchiveStep(7, false);

                    if (ArrayPuzzlePieces[1] != null)
                        ArrayPuzzlePieces[1] = null;

                }
            }
        }
        
 
    }

}
