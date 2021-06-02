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

    private objectController mesaCon;
    private Transform mesaTransform;

    private bool check;
    private bool canFinish;

    private BoxCollider[] collidersFilhos;

    private void Awake()
    {
        mesaCon = GetComponentInParent<objectController>();
        mesaTransform = transform.parent;
    }

    private void OnEnable()
    {
        mesaCon.OnInteraction += DisableColliders;
    }

    private void OnDisable()
    {
        mesaCon.OnInteraction -= DisableColliders;
    }

    private void Start()
    {
        locker = new bool[4];
        encaixe = new Transform[4];

        for(int i = 0; i < encaixe.Length; i++)
        {
            encaixe[i] = transform.GetChild(i);
            locker[i] = false;
        }

        collidersFilhos = GetComponentsInChildren<BoxCollider>();
    }

    private void DisableColliders(object sender, EventArgs e)
    {
        foreach(BoxCollider a in collidersFilhos)
        {
            a.enabled = false;
        }
        
        check = true;
        StartCoroutine(EnableAgain());
    }

    private void EnableColliders()
    {
        foreach (BoxCollider a in collidersFilhos)
        {
            a.enabled = true;
        }

        check = false;
    }

    private IEnumerator EnableAgain()
    {
        while (check)
        {
            if (!mesaCon.hasInteracted)
            {
                check = false;
                EnableColliders();
            }

            yield return null;
        }

        yield return null;
    }

    public override void MiddleStep()
    {
        for (int i = 0; i < steps.Length - 5; i += 2)
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
        if (CheckStep(12))
            return;

        if (!ArrayPuzzlePieces.Any(element => element != null))
            return;

        for (int i = 0; i < 4; i++)
        {
            if (CheckStep(i + 8))
                continue;

            if (ArrayPuzzlePieces[i] == null)
                continue;

            Transform peMesa = ArrayPuzzlePieces[i].transform;

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

            AchiveStep(2 * i, false);
            AchiveStep(2 * i + 1, false);

            if (ArrayPuzzlePieces[i] != null)
                ArrayPuzzlePieces[i] = null;

            locker[i] = false;
        }
    }

    //bom é isso aqui por enquanto msm
    private bool setTheParentNull = false;
    private void LateUpdate()
    {
        if (setTheParentNull)
        {
            mesaTransform.SetParent(null);
            setTheParentNull = false;
            if (mesaTransform.GetComponent<EPOOutline.Outlinable>())
                Destroy(mesaTransform.GetComponent<EPOOutline.Outlinable>());
            if (mesaTransform.GetComponent<Rigidbody>())
                mesaTransform.GetComponent<Rigidbody>().isKinematic = false;
        }
        
    }

    // chamado pelo object controller quando o jogador começa a rotacionar a mesa
    public override void FinalStep()
    {
            
        Destroy(mesaCon);

        mesaTransform.localEulerAngles = new Vector3(UnityEngine.Random.Range(-2f, 2f), mesaTransform.localEulerAngles.y, UnityEngine.Random.Range(-2f, 2f));
 
        mesaTransform.tag = "Untagged";

        setTheParentNull = true;

        AchiveStep(12, true);
     
    }

    private void OnTriggerEnter(Collider other)
    {
        canFinish = CheckStep(8) && CheckStep(9) && CheckStep(10) && CheckStep(11) && !CheckStep(12);

        if (canFinish)
        {
            FinalStep();
        }

    }

}
