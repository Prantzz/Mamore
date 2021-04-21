using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameGlobeData : MonoBehaviour
{
    public event EventHandler OnSceneStart;
    public event EventHandler OnSceneEnd;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameResumed;
    public event EventHandler OnEscPressed;
    public static GameGlobeData GameCon;
    public static bool SceneHasEnded = false;
    public static bool IsGamePaused = false;
    public static bool isCompassCollected = false;
    public static bool IsCamLock = false;
    public static bool IsDocumentCollected = false;
    public static bool IsGameOver = false;
    public static bool IsGoodEnding = true;
    public static ParticleSystem[] PSList;

    private GameObject player;
    private int currentTutorial;
    private bool[] TutorialConditions;
    private Queue<int> TutorialBehaviourFila;

    [SerializeField]
    private InventoryObject InvObj;
    [SerializeField]
    private InventoryDocument InvDoc;

    private Image thisImgIn;
    private Image thisImgOut;    
    float fadeInAlphaVal;
    float fadeOutAlphaVal;
    public GameObject fadeInSceneCanvas;
    public GameObject fadeOutSceneCanvas;

    private void Awake()
    {
        //Criando singleton
        GameCon = this;
    }
    private void Start()
    {
        TutorialBehaviourFila = new Queue<int>();
        DontDestroyOnLoad(gameObject);
        OnSceneStart += GameCon_OnSceneStart;
        OnSceneEnd += GameCon_OnSceneEnd;
        SceneManager.activeSceneChanged += GetAllPS;
        reallyOnTutorialTrigger += changeTutorialConditions;

    }
    void Update()
    {
        
        if (Time.timeSinceLevelLoad <= 1.5) OnSceneStart?.Invoke(this, EventArgs.Empty);
        if (SceneHasEnded) OnSceneEnd?.Invoke(this, EventArgs.Empty);
        if (IsGamePaused) OnGamePaused?.Invoke(this, EventArgs.Empty);
        if (!IsGamePaused) OnGameResumed?.Invoke(this, EventArgs.Empty);       
        if (Input.GetKeyDown(KeyCode.Escape)) OnEscPressed?.Invoke(this, EventArgs.Empty);

        //--------------------------DEBUG---------------------------------------
        if (Input.GetKeyDown(KeyCode.Z) && SceneManager.GetActiveScene().buildIndex == 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);
        if (Input.GetKeyDown(KeyCode.C)) this.GetComponent<AudioManager>().PS.InvokeSound();
        //-----------------------------------------------------------------------


        if(fadeInSceneCanvas == null && GameObject.Find("FadeInBlack") != null) 
        {
            fadeInSceneCanvas = GameObject.Find("FadeInBlack");          
        }
        if (fadeOutSceneCanvas == null && GameObject.Find("FadeOutBlack") != null) 
        {
            fadeOutSceneCanvas = GameObject.Find("FadeOutBlack");   
        }

        //Caso a condição de tutorial seja diferente de zero cheque todo frame caso o player completou o tutorial;
        ///Não é ideial fazer dessa maneira, seria correto ter um event para cada ação possível mas foda-se
        if(currentTutorial!=0) checkTutorialConditions();

        /*Debug.Log(fadeInAlphaVal + " - fade In AlphaVal");
        Debug.Log(fadeOutAlphaVal + " - fade out AlphaVal");*/
    }

    public static void FreezeGame() 
    {
        Time.timeScale = 0f;
    }
    public static void UnfreezeGame()
    {
        Time.timeScale = 1f;
    }

    private void GameCon_OnSceneEnd(object sender, System.EventArgs e)
    {
        if(fadeOutAlphaVal >= 1) fadeOutAlphaVal = 0f;
        if (fadeOutSceneCanvas != null)
        {
            fadeOutSceneCanvas.SetActive(true);
            thisImgOut = fadeOutSceneCanvas.GetComponent<Image>();
            thisImgOut.color = new Color(thisImgIn.color.r, thisImgIn.color.g, thisImgIn.color.b, fadeOutAlphaVal);
            fadeOutAlphaVal += 0.8f * Time.deltaTime;

            if(fadeOutAlphaVal >= 1) 
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                fadeOutAlphaVal = 1f;
                SceneHasEnded = false;
            }
        }
        
    }

    private void GameCon_OnSceneStart(object sender, System.EventArgs e)
    {
        SceneHasEnded = false;
        if(Time.timeSinceLevelLoad <= 0) fadeInAlphaVal = 1f;

        if (fadeOutSceneCanvas != null) fadeOutSceneCanvas.SetActive(false);

        if (fadeInSceneCanvas != null) 
        {
            thisImgIn = fadeInSceneCanvas.GetComponent<Image>();
            thisImgIn.color = new Color(thisImgIn.color.r, thisImgIn.color.g, thisImgIn.color.b, fadeInAlphaVal);
            fadeInAlphaVal -= 0.8f * Time.deltaTime;

            if (fadeInAlphaVal <= 0)
            {
                fadeInSceneCanvas.SetActive(false);
                fadeInAlphaVal = 0;
            }
        }
    }

    private void GetAllPS(Scene cur, Scene next) 
    {
        //Caso seja a cena principal
        if(next.buildIndex == 2)
        {
            player = GameObject.Find("Player");
            //Busque todos os Particle system do mapa e ponha na lista
            PSList = Resources.FindObjectsOfTypeAll<ParticleSystem>();
        }
    }

    #region Tutorial Stuff
    //Declaração do event e delegate de trigger de tutorial-----
    public delegate void onTutorialTrigger(int behaviourType, bool newOrNot);
    public event onTutorialTrigger reallyOnTutorialTrigger;
    //----------------------------------------------------------

    //Função do trigger de tutorial
    public void TutorialTrigger(int behaviour, bool newOrNot)
    {
        if(reallyOnTutorialTrigger != null)
        {
            reallyOnTutorialTrigger(behaviour,newOrNot);
        }
    }

    //Declaração do evento de completar tutorial--------------
    public delegate void onTutorialCompleted();
    public event onTutorialCompleted reallyOnTutorialCompleted;
    //--------------------------------------------------------

    public void TutorialCompleted()
    {
        if (reallyOnTutorialCompleted != null)
        {
            reallyOnTutorialCompleted();
        }
    }

    #region Métodos Condicionais de Tutorial
    //Checa para ver se as condições para compeltar o tutorial foram preenchidas
    private void checkTutorialConditions()
    {        
        switch (currentTutorial)
        {
            case (1):
                if (Input.GetKeyDown(KeyCode.W)) TutorialConditions[0] = true;
                if (Input.GetKeyDown(KeyCode.A)) TutorialConditions[1] = true;
                if (Input.GetKeyDown(KeyCode.S)) TutorialConditions[2] = true;
                if (Input.GetKeyDown(KeyCode.D)) TutorialConditions[3] = true;
                break;
            case (2):
                if (Input.GetKeyDown(KeyCode.Space)) TutorialConditions[0] = true;
                break;
        }
        bool passer = true;
        foreach(bool b in TutorialConditions)
        {
            if (!b)
            {
                passer = false;
                break;
            }
        }
        if (passer)
        {
            changeTutorialConditions(0,false);
        }
    }
    public void AlterTutorialCondition()
    {
        for (int i = 0; i < TutorialConditions.Length; i++)
        {
            if (!TutorialConditions[i])
            {
                TutorialConditions[i] = true;
                return;
            }
        }
    }
    //Muda as condições para completar o atual tutorial
    private void changeTutorialConditions(int behaviour, bool newValue)
    {
        if (TutorialBehaviourFila.Count > 0 && behaviour == 0)
        {            
            TutorialCompleted();
            changeTutorialConditions(TutorialBehaviourFila.Dequeue(),false);
            return;
        }
        if (currentTutorial != 0 && behaviour != 0 && newValue)
        {
            TutorialBehaviourFila.Enqueue(behaviour);
            return;
        }
        else
        {
            switch (behaviour)
            {
                case (0):
                    currentTutorial = behaviour;
                    TutorialConditions = new bool[0];
                    TutorialCompleted();
                    break;
                case (1):
                    currentTutorial = behaviour;
                    TutorialConditions = new bool[4];
                    break;
                case (2):
                    currentTutorial = behaviour;
                    TutorialConditions = new bool[1];
                    break;
                case (3):
                    currentTutorial = behaviour;
                    TutorialConditions = new bool[1];
                    break;
            }
        }        
    }

    #endregion

    #endregion

    #region APPLICATION.QUIT
    private void OnApplicationQuit()
    {
        InvDoc.Container.Clear();
        InvObj.Container.Clear();
    }
    #endregion

}
