using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePuzzleCollider : MonoBehaviour
{
    [SerializeField]
    private int stepOnConllision;
    public Puzzle puzzle;
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
        if (!this.CompareTag("PuzzleCollider")) Debug.LogError("A tag deste collider n�o � 'Puzzle Collider'", this);
        if (!this.GetComponent<Collider>().isTrigger) Debug.LogError("Esse collider n�o � trigger", this);
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
        //Debug.Log(other.tag);
        if (canCollide)
        {
            if (other.CompareTag(tagToCheck))
            {

                if (other.GetComponentInParent<objectController>()?.type == correctObject)
                {
                    //Debug.Log(other.transform.parent.gameObject);
                    puzzle.AddPiece(other.transform.parent.gameObject);
                    puzzle.AchiveStep(stepOnConllision, true);
                }
            }
        }        
    }
    private void OnTriggerExit(Collider other)
    {
        if (canCollide)
        {
            if (other.CompareTag(tagToCheck))
            {
                if (other.GetComponentInParent<objectController>()?.type == correctObject)
                {
                    puzzle.AddPiece(other.transform.parent.gameObject);
                    puzzle.AchiveStep(stepOnConllision, false);
                }
            }
        }        
    }
    public void TryToAchiveStep()
    {
        //Aqui estou dizendo que ele s� pode ativar com uma tool depois que colidiu, imagino que esse n�o seja o caso para todo puzzle mas n�o farei esse modifica��o sem necessidade
        //N�o gosto muito disso, esse c�digo � muito espec�fico e deveria estar no Puzzle n�o no collider.
        if (puzzle.CheckStep(stepOnConllision) && !puzzle.CheckStep(stepOnTool)) puzzle.AchiveStep(stepOnTool, true);
    }
    public void AchiveTalkStep()
    {
        puzzle.AchiveStep(stepOnTool,true);
    }
}
