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
    void Start()
    {
        GameGlobeData.GameCon.reallyOnTutorialTrigger += ChangeHud;
        GameGlobeData.GameCon.reallyOnTutorialCompleted += DesapearHud;
        ownAnim = GetComponent<Animator>();
        tutorialTexts = new string[] {"Mova-se com WASD"};
    }
    private void ChangeHud(int behaviour)
    {
        //Caso ele n�o tenha nenhum tutorial 
        if (!ownAnim.GetBool("Visivel"))
        {
            //Ligue a HUD
            ownAnim.SetBool("Visivel", true);
            //Atribua o texto de acordo com o behaviour
            tutorialTextCanvas.text = tutorialTexts[behaviour-1];
        }
        //Caso ele ja tenha um tutorial aberto e � apenas uma continua��o
        else
        {

        }
        
        Debug.Log("Chamando o texto de tutorial de n�mero: " + behaviour);
    }
    private void DesapearHud()
    {
        ownAnim.SetBool("Visivel", false);
    }
}
