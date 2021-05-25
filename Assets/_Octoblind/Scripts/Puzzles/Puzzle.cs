using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public int index;
    [SerializeField]
    protected bool[] steps;
    public bool completed;
    public List<GameObject> PuzzlePieces;

    [Header("N�o mexa nesse array sem entend�-lo, utilize a lista de cima")] 
    [Space]
    public GameObject[] ArrayPuzzlePieces;

    private void Start()
    {
        
        if (steps == null) Debug.LogError("ESSE PUZZLE N�O TEM STEPS CARALHO!",this);
        
    }

    //Chamado pelo simple puzzle collider
    public void AchiveStep(int step, bool state)
    {
        steps[step] = state;
        MiddleStep(step);
        MiddleStep();
        if (state)this.completed = CheckForCompletion();
    }

    private bool CheckForCompletion()
    {
        foreach(bool b in this.steps)
        {
            if (!b) return false;
        }
        return true;
    }

    public virtual void MiddleStep(int step = 0) { }
    public virtual void MiddleStep() { }

    //chamado pelo simple puzzle collider
    public void AddPiece(GameObject toAdd)
    {
        if(!PuzzlePieces.Contains(toAdd))PuzzlePieces.Add(toAdd);
    }

    /// <summary>
    /// Tenha certeza de ter o array j� pronto com os indexes preparados antes de chamar essa sobrecarga.
    /// </summary>
    /// <param name="toAdd"></param>
    /// <param name="atIndex"></param>
    public void AddPiece(GameObject toAdd, int atIndex) 
    {
        // usei array aqui msm pq com lista ela n�o preenche espa�os nulos, ela s� cria um espa�o no meio de outros indexes e n�o funcionou ao prop�sito
        if (!Array.Exists(ArrayPuzzlePieces, element => element == toAdd)) ArrayPuzzlePieces[atIndex] = toAdd; 
    }

    //chamado pelo simple puzzle collider
    public void RemovePiece(GameObject toRemove)
    {
        PuzzlePieces.Remove(toRemove);
    }

    public void ArrayRemovePiece(GameObject toRemove)
    {
        Debug.Log(toRemove);

        int a = Array.FindIndex(ArrayPuzzlePieces, element => element == toRemove);
        if(a < ArrayPuzzlePieces.Length)
            ArrayPuzzlePieces[a] = null;
    }


    public GameObject GetPiece (int pieceIndex)
    {
        return PuzzlePieces[pieceIndex];

    }

    //Recomendado utilizar esse m�todo em conjunto com ArrayPuzzlePieces
    public virtual void DesajustarParte(GameObject objeto) { }

    //Fiz diversas punheta��es de prog aqui para pegar uma exce��o mas admito que estou ficando com sono e isso n�o � essencial.
    //Atualmente da um IndexOutofBound mas pelo menos dou esse error tamb�m.
    //TLDR: Se tu colocar no collider que ele da achive num step que n�o est� no array o sistema n�o tem como identificar, � isso.
    
    public bool CheckStep(int stepToCheck) 
    {
        return (stepToCheck <= steps.Length - 1) ? steps[stepToCheck] : ThrowError();
        bool ThrowError()
        {
            Debug.LogError("Voc� est� tentando verificar um step que n�o existe nesse Puzzle", this);
            return false;
        }
    }

}

