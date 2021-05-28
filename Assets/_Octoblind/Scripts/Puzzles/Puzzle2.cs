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
            Debug.Log(i);
            if (CheckStep(i) && CheckStep(i+1) && !locker[i/2])
            {
                Debug.Log("chamou o desajustar e afins");
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
            Debug.Log(a.name);
            if (!a.gameObject.activeSelf)
                a.gameObject.SetActive(true);
        }
        

    }

    private void AjustarPemesa(Vector3 pos, Transform peMesa)
    {
        Debug.Log("Ajustou o: " + peMesa);
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

        Destroy(peMesa.GetComponent<EPOOutline.Outlinable>()); // isso aqui não sei pq não sumiu sozinho
        Destroy(peMesa.GetComponent<Rigidbody>()); // tentei usar isKinematic = true, mas ele da conflito com o rigidbody do objeto pai
    }

    public override void DesajustarParte(GameObject peMesa)
    {
        Debug.Log("chamou o metodo desajustar");
        if (ArrayPuzzlePieces.Any())
        {
            Debug.Log("chamou a funcionalidade desajustar ");

            for (int i = 0; i < 4; i++)
            {
                if (Array.IndexOf(ArrayPuzzlePieces, peMesa) != i)
                    continue;
                if (CheckStep(i + 4)) // Step correspondente à martelada
                    continue;

                Debug.Log($"desajustou esse pemesa: {peMesa}");
                Transform peMesaTransform = peMesa.transform;
                peMesaTransform.SetParent(null);
                peMesaTransform.GetComponent<objectController>().enabled = true;
                peMesaTransform.GetComponent<BoxCollider>().enabled = false;
                peMesaTransform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
                peMesaTransform.GetChild(2).GetComponent<BoxCollider>().enabled = false;
                peMesa.AddComponent(typeof(Rigidbody));

            }

        }

    }

}
