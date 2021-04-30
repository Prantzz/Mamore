using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePuzzleCollider : MonoBehaviour
{
    [SerializeField]
    private int stepOnConllision;
    public Puzzle puzzle;
    public bool canCollide;
    public string correctTool;
    public int stepOnTool;
    private void Start()
    {
        //-----LOG ERROR-----
        if (!this.CompareTag("PuzzleCollider")) Debug.LogError("A tag deste collider não é 'Puzzle Collider'", this);

        if(GetComponentInParent<Puzzle>() != null)
        {
            puzzle = GetComponentInParent<Puzzle>();
        }
        else
        {
            Debug.LogError("Esse collider não tem um puzzle part como parent!",this);
        }
        //--------------------

        canCollide = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (canCollide)
        {
            if (other.CompareTag("Trigger"))
            {
                puzzle.AddPiece(other.transform.parent.gameObject);
                puzzle.AchiveStep(stepOnConllision, true);
            }
        }        
    }
    private void OnTriggerExit(Collider other)
    {
        if (canCollide)
        {
            if (other.CompareTag("Trigger"))
            {
                puzzle.RemovePiece(other.transform.parent.gameObject);
                puzzle.AchiveStep(stepOnConllision, false);
            }
        }        
    }
    public void TryToAchiveStep()
    {
        //Aqui estou dizendo que ele só pode ativar com uma tool depois que colidiu, imagino que esse não seja o caso para todo puzzle mas não farei esse modificação sem necessidade
        //Não gosto muito disso, esse código é muito específico e deveria estar no Puzzle não no collider.
        if (puzzle.CheckStep(stepOnConllision) && !puzzle.CheckStep(stepOnTool)) puzzle.AchiveStep(stepOnTool, true);
    }
}
