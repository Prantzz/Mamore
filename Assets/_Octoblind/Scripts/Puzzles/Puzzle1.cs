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
            
            AjustarDegrau(finalPosDegrau1.position, PuzzlePieces[a]);
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

            AjustarDegrau(finalPosDegrau2.position, PuzzlePieces[a]);
            Degrau1.GetChild(0).GetComponent<SimplePuzzleCollider>().canCollide = false;
            Degrau1.GetChild(1).GetComponent<SimplePuzzleCollider>().canCollide = false;
            locker2 = true;
        }

    }

    public void AjustarDegrau(Vector3 pos, GameObject degrau)
    {
        a++;

        degrau.transform.SetParent(transform);
        degrau.transform.position = pos;
        degrau.transform.eulerAngles = new Vector3(0, 90, 0);
        degrau.GetComponent<objectController>().enabled = false;
        degrau.GetComponent<BoxCollider>().enabled = false;
        degrau.transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
        degrau.transform.GetChild(2).GetComponent<BoxCollider>().enabled = false;
        degrau.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
     

    }

    public void DesajustarDegrau()
    {
        a = 0;

        if ((!CheckStep(4) || !CheckStep(5)) && PuzzlePieces[0])
        {
            Debug.Log("desajustou degrau1");
            PuzzlePieces[0].transform.SetParent(null);
            PuzzlePieces[0].GetComponent<objectController>().enabled = true;
            PuzzlePieces[0].GetComponent<BoxCollider>().enabled = true;
            PuzzlePieces[0].transform.GetChild(1).GetComponent<BoxCollider>().enabled = true;
            PuzzlePieces[0].transform.GetChild(2).GetComponent<BoxCollider>().enabled = true;
            PuzzlePieces[0].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            PuzzlePieces[0].GetComponent<Rigidbody>().isKinematic = false;
            PuzzlePieces[0].transform.eulerAngles = new Vector3(0, 45, 0);
            PuzzlePieces[0].GetComponent<objectController>().isSelected = false;
            PuzzlePieces[0].GetComponent<objectController>().hasInteracted = false;
            Degrau0.GetChild(0).GetComponent<SimplePuzzleCollider>().canCollide = true;
            Degrau0.GetChild(1).GetComponent<SimplePuzzleCollider>().canCollide = true;
            locker1 = false;
            Degrau0.GetComponent<OnTriggerEnterEvent>().AlternateTrigger(false);
            if (!Degrau1.gameObject.activeSelf)
                Degrau1.gameObject.SetActive(true);

            PuzzlePieces.RemoveAt(0);

            AchiveStep(0, false);
            AchiveStep(1, false);
        }
        if (!CheckStep(5) || !CheckStep(6) && PuzzlePieces.Count > 1)
        {
           
            Debug.Log("desajustou degrau2");
            PuzzlePieces[1].transform.SetParent(null);
            PuzzlePieces[1].GetComponent<objectController>().enabled = true;
            PuzzlePieces[1].GetComponent<BoxCollider>().enabled = true;
            PuzzlePieces[1].transform.GetChild(1).GetComponent<BoxCollider>().enabled = true;
            PuzzlePieces[1].transform.GetChild(2).GetComponent<BoxCollider>().enabled = true;
            PuzzlePieces[1].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            PuzzlePieces[1].GetComponent<Rigidbody>().isKinematic = false;
            PuzzlePieces[1].transform.eulerAngles = new Vector3(0, 45, 0);
            PuzzlePieces[1].GetComponent<objectController>().isSelected = false;
            PuzzlePieces[1].GetComponent<objectController>().hasInteracted = false;

            PuzzlePieces.RemoveAt(1);
           

            Degrau1.GetChild(0).GetComponent<SimplePuzzleCollider>().canCollide = true;
            Degrau1.GetChild(1).GetComponent<SimplePuzzleCollider>().canCollide = true;
            locker2 = false;

            Degrau1.GetComponent<OnTriggerEnterEvent>().AlternateTrigger(false);
            if (!Degrau0.gameObject.activeSelf)
                Degrau0.gameObject.SetActive(true);

            AchiveStep(2, false);
            AchiveStep(3, false);
        }
        
        
            

    }

}
