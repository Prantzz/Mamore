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
    private void Start()
    {
        if (steps == null) Debug.LogError("ESSE PUZZLE N�O TEM STEPS CARALHO!",this);
        //PuzzlePieces = new List<GameObject>();
    }

    //Chamado pelo simple puzzle collider
    public void AchiveStep(int step, bool state)
    {
        steps[step] = state;
        MiddleStep();
        if(state)this.completed = CheckForCompletion();
    }

    private bool CheckForCompletion()
    {
        foreach(bool b in this.steps)
        {
            if (!b) return false;
        }
        return true;
    }

    public virtual void MiddleStep() { }

    //chamado pelo simple puzzle collider
    public void AddPiece(GameObject toAdd)
    {
        if(!PuzzlePieces.Contains(toAdd))PuzzlePieces.Add(toAdd);
    }

    //chamado pelo simple puzzle collider
    public void RemovePiece(GameObject toRemove)
    {
        PuzzlePieces.Remove(toRemove);
    }

    public GameObject GetPiece (int pieceIndex)
    {
        return PuzzlePieces[pieceIndex];

    }

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
