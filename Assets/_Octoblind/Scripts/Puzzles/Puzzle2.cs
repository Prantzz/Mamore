using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed public class Puzzle2 : Puzzle
{
    //Puzzle da mesa

    [SerializeField] private Transform[] finalPosPemesa;

    private Transform[] encaixe;

    private bool[] locker;

    private void Start()
    {
        locker = new bool[4];
        encaixe = new Transform[4];

        for(int i = 0; i < encaixe.Length; i++)
        {
            encaixe[i] = transform.GetChild(i);
            locker[i] = false;
        }
    }

    public override void MiddleStep()
    {
        for (int i = 0; i < steps.Length - 4; i += 2)
        {
            if (CheckStep(i) && CheckStep(i+1) && !locker[i/2])
            {
                encaixe[i/2].GetComponent<OnTriggerEnterEvent>().AlternateTrigger(true);

                EnableTheDisabled(i/2);
                
                ArrayPuzzlePieces[i/2].GetComponent<objectController>().hasInteracted = false;
                AjustarPemesa(finalPosPemesa[i/2].position, ArrayPuzzlePieces[i/2].transform);
                encaixe[i/2].GetChild(0).GetComponent<SimplePuzzleCollider>().canCollide = false;
                encaixe[i/2].GetChild(1).GetComponent<SimplePuzzleCollider>().canCollide = false;

                locker[i/2] = true;
            }
            
        }

        ////Travar perna 0
        //if (CheckStep(0) && !locker[0])
        //{

        //    locker[0] = true;
        //}

        ////Travar perna 1
        //if (CheckStep(1) && !locker[1])
        //{
        //    locker[1] = true;
        //}


        ////Travar perna 2
        //if (CheckStep(2) && !locker[2])
        //{
        //    locker[2] = true;
        //}


        ////Travar perna 3
        //if (CheckStep(3) && !locker[3])
        //{
        //    locker[3] = true;
        //}

    }

    private void EnableTheDisabled(int i)
    {
        Transform[] rightFeet;

        //linq salvando vidas aqui
        rightFeet = encaixe.Where(a => a != encaixe[i]).ToArray();

        foreach (Transform a in rightFeet)
        {
            if (!a.gameObject.activeSelf)
                a.gameObject.SetActive(true);
        }
        

    }

    private void AjustarPemesa(Vector3 pos, Transform peMesa)
    {
        objectController objCon = peMesa.GetComponent<objectController>();
        peMesa.SetParent(transform);
        peMesa.position = pos;
        peMesa.eulerAngles = new Vector3(0, 0, 0);
        objCon.isSelected = false;
        objCon.hasInteracted = false;
        objCon.enabled = false;

        peMesa.GetComponent<BoxCollider>().enabled = false;
        peMesa.GetChild(1).GetComponent<BoxCollider>().enabled = false;
        peMesa.GetChild(2).GetComponent<BoxCollider>().enabled = false;

        Destroy(peMesa.GetComponent<EPOOutline.Outlinable>()); // isso aqui não sei pq não sumiu sozinho, então to destruindo pra garantir

        Destroy(peMesa.GetComponent<Rigidbody>()); 
        // tentei usar isKinematic = true, mas ele da conflito com o rigidbody do objeto pai
        // tbm tentei usar fixed joint/hinge joint, mas ai a gravidade parava de afetar o objeto pai
        // então o esquema foi remover msm o rigidbody e adicionar dnv quando o pe da mesa cair
    }

    public override void DesajustarParte()
    {

        if (ArrayPuzzlePieces.Any(element => element != null))
        {

            for (int i = 0; i < 4; i++)
            {
                if (ArrayPuzzlePieces[i] == null)
                    continue;
                
                Transform peMesa = ArrayPuzzlePieces[i].transform;

                if (!CheckStep(i + 8))
                {
                    
                    Transform encaixe = this.encaixe[i];
                    objectController objCon = peMesa.GetComponent<objectController>();
                    peMesa.SetParent(null);
                    peMesa.eulerAngles = new Vector3(UnityEngine.Random.Range(25, 60), 0, UnityEngine.Random.Range(25, 60));
                    objCon.enabled = true;
                    objCon.isSelected = false;
                    objCon.hasInteracted = false;
                    peMesa.GetComponent<BoxCollider>().enabled = true;
                    peMesa.GetChild(1).GetComponent<BoxCollider>().enabled = true;
                    peMesa.GetChild(2).GetComponent<BoxCollider>().enabled = true;
                    peMesa.gameObject.AddComponent(typeof(Rigidbody));

                    encaixe.GetChild(0).GetComponent<SimplePuzzleCollider>().canCollide = true;
                    encaixe.GetChild(1).GetComponent<SimplePuzzleCollider>().canCollide = true;
                    encaixe.GetComponent<OnTriggerEnterEvent>().AlternateTrigger(false);
                    EnableTheDisabled(i);

                    AchiveStep(2*i, false);
                    AchiveStep(2*i + 1, false);

                    if (ArrayPuzzlePieces[i] != null)
                        ArrayPuzzlePieces[i] = null;

                    locker[i] = false;

                }

            }

        }

    }


}
