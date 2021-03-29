using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    private GameGlobeData GameCon;
    private GameObject PauseMenu;
    private AudioManager AU;

    void Start()
    {
        PauseMenu = GameObject.Find("PausePanel");
        GameCon = GameObject.Find("GameGlobeData").GetComponent<GameGlobeData>();
        AU = GameCon.GetComponent<AudioManager>();

        PauseMenu.SetActive(false);

        GameCon.OnGamePaused += GameCon_OnGamePaused;
        GameCon.OnGameResumed += GameCon_OnGameResumed;
        GameCon.OnEscPressed += GameCon_OnEscPressed;
    }
    private void GameCon_OnGameResumed(object sender, System.EventArgs e)
    {
        if(PauseMenu != null) 
        { 
        GameGlobeData.IsCamLock = false;
        PauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        GameGlobeData.UnfreezeGame();
        }
    }

    private void GameCon_OnEscPressed(object sender, System.EventArgs e)
    {
        if (GameGlobeData.IsGamePaused)
        {
            AU.ResumeSound();
            GameGlobeData.IsGamePaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
        else
        {
            AU.PauseSound();
            GameGlobeData.IsGamePaused = true;
        }
    }

    private void GameCon_OnGamePaused(object sender, System.EventArgs e)
    {
        GameGlobeData.FreezeGame();
        
        PauseMenu.SetActive(true);
        GameGlobeData.IsCamLock = true;
        Cursor.lockState = CursorLockMode.None;
    }

}
