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
    public static bool SceneHasEnded = false;
    public static bool IsGamePaused = false;
    public static bool isCompassCollected = false;
    public static bool IsCamLock = false;
    public static bool IsDocumentCollected = false;
    public static bool IsGameOver = false;
    public static ParticleSystem[] PSList;
    private Image thisImgIn;
    private Image thisImgOut;
    private GameGlobeData GameCon;
    float fadeInAlphaVal;
    float fadeOutAlphaVal;
    public GameObject fadeInSceneCanvas;
    public GameObject fadeOutSceneCanvas;


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        OnSceneStart += GameCon_OnSceneStart;
        OnSceneEnd += GameCon_OnSceneEnd;
        SceneManager.activeSceneChanged += GetAllPS;

    }
    void Update()
    {
        
        if (Time.timeSinceLevelLoad <= 5) OnSceneStart?.Invoke(this, EventArgs.Empty);
        if (SceneHasEnded) OnSceneEnd?.Invoke(this, EventArgs.Empty);
        if (IsGamePaused) OnGamePaused?.Invoke(this, EventArgs.Empty);
        if (!IsGamePaused) OnGameResumed?.Invoke(this, EventArgs.Empty);       
        if (Input.GetKeyDown(KeyCode.Escape)) OnEscPressed?.Invoke(this, EventArgs.Empty);

        //--------------------------DEBUG---------------------------------------
        if (Input.GetKeyDown(KeyCode.Z)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);
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
        if(fadeOutSceneCanvas != null)fadeOutSceneCanvas.SetActive(false);
        
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
            //Busque todos os Particle system do mapa e ponha na lista
            PSList = Resources.FindObjectsOfTypeAll<ParticleSystem>();
        }
    }
}
