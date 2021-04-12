using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCanvasComponent : MonoBehaviour
{
    private Animator ownAnim;
    [SerializeField]
    private Text tutorialTextCanvas;
    private string[] tutorialTexts;
    private Queue<string> queuedTutorials;
    void Start()
    {
        queuedTutorials = new Queue<string>();
        GameGlobeData.GameCon.reallyOnTutorialTrigger += ChangeHud;
        GameGlobeData.GameCon.reallyOnTutorialCompleted += DesapearHud;
        GameGlobeData.GameCon.reallyOnTutorialCompleted += ChangeHud;

        ownAnim = GetComponent<Animator>();
        tutorialTexts = new string[] {"Mova-se com WASD", "Pule com Espaço"};
    }
    private void Update()
    {
        
    }
    private void DesapearHud()
    {
        //Caso não existam itens na fila
        //if (queuedTutorials.Count <= 0) ownAnim.SetBool("Visivel", false);
    }
    private void ChangeHud()
    {
        if (queuedTutorials.Count > 0)
        {
            ownAnim.SetTrigger("ChangeAnimTrigger");
        }
    }
    public void ChangeHudText()
    {
        tutorialTextCanvas.text = queuedTutorials.Dequeue();
    }
    private void ChangeHud(int behaviour)
    {
        //Caso ele não tenha nenhum tutorial 
        if (!ownAnim.GetBool("Visivel"))
        {
            //Ligue a HUD
            ownAnim.SetBool("Visivel", true);
            //Atribua o texto de acordo com o behaviour
            tutorialTextCanvas.text = tutorialTexts[behaviour-1];
        }
        //Caso ele ja tenha um tutorial aberto coloque o tutorial na queue
        else
        {
            queuedTutorials.Enqueue(tutorialTexts[behaviour - 1]);
        }
    }
    
}
