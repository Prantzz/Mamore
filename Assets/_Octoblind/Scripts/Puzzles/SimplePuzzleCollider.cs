using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePuzzleCollider : MonoBehaviour
{
    [SerializeField]
    private int stepOnConllision;
    public Puzzle puzzle;
    [SerializeField]private int giveIndex;
    public bool canCollide;
    public string tagToCheck;
    [SerializeField]
    private string correctObject;
    public string correctTool;
    public int stepOnTool;
    public bool talker;
    private void Start()
    {
        //-----LOG ERROR-----
        if (!this.CompareTag("PuzzleCollider")) Debug.LogError($"A tag deste collider \"{gameObject.name}\" n�o � 'Puzzle Collider'", this);
        if (!this.GetComponent<Collider>().isTrigger) Debug.LogError($"Esse collider \"{gameObject.name}\" n�o � trigger", this);
        if(GetComponentInParent<Puzzle>() != null)
        {
            puzzle = GetComponentInParent<Puzzle>();
        }
        else
        {
            Debug.LogError("Esse collider n�o tem um puzzle part como parent!",this);
        }
        //--------------------

        canCollide = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canCollide) //fiz invers�es de if aqui s� pelo prazer
            return;

        if (!other.CompareTag(tagToCheck))
            return;

        if (other.GetComponentInParent<objectController>()?.type == correctObject)
        {
            if(puzzle.ArrayPuzzlePieces.Any()) //usei Linq aqui s� pelo prazer tamb�m
            {
                puzzle.ArrayAddPiece(other.transform.parent.gameObject, giveIndex);
                puzzle.AchiveStep(stepOnConllision, true);
            }
            else if (!puzzle.ArrayPuzzlePieces.Any())
            {
                puzzle.AddPiece(other.transform.parent.gameObject);
                puzzle.AchiveStep(stepOnConllision, true);
            }
            else
            {
                Debug.Log("SOMETHING GONE WRONG");
            }                      
        }  
    }

    private void OnTriggerExit(Collider other)
    {
        if (!canCollide)
            return;

        if (!other.CompareTag(tagToCheck))
            return;
        
        if (other.GetComponentInParent<objectController>()?.type == correctObject)
        {
            if (puzzle.ArrayPuzzlePieces.Any())
            {
                puzzle.ArrayRemovePiece(other.transform.parent.gameObject);
                puzzle.AchiveStep(stepOnConllision, false);
            }
            else if(!puzzle.ArrayPuzzlePieces.Any())
            {
                puzzle.RemovePiece(other.transform.parent.gameObject);
                puzzle.AchiveStep(stepOnConllision, false);
            }
            else
            {
                Debug.Log("SOMETHING GONE WRONG");
            }  
        }
    }


    public void TryToAchiveStep()
    {

        //Aqui estou dizendo que ele s� pode ativar com uma tool depois que colidiu, imagino que esse n�o seja o caso para todo puzzle mas n�o farei esse modifica��o sem necessidade
        //N�o gosto muito disso, esse c�digo � muito espec�fico e deveria estar no Puzzle n�o no collider.

        if (puzzle.CheckStep(stepOnConllision) && !puzzle.CheckStep(stepOnTool))
        {
            puzzle.AchiveStep(stepOnTool, true);
           
        }
    }
    public void AchiveTalkStep()
    {
        puzzle.AchiveStep(stepOnTool,true);
    }
}
