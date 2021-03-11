using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    private GameGlobeData GameCon;
    private GameObject PauseMenu;

    void Start()
    {
        PauseMenu = GameObject.Find("PausePanel");
        GameCon = GameObject.Find("GameGlobeData").GetComponent<GameGlobeData>();

        PauseMenu.SetActive(false);

        GameCon.OnGamePaused += GameCon_OnGamePaused;
        GameCon.OnGameResumed += GameCon_OnGameResumed;
        GameCon.OnEscPressed += GameCon_OnEscPressed;
    }
    private void GameCon_OnGameResumed(object sender, System.EventArgs e)
    {
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void GameCon_OnEscPressed(object sender, System.EventArgs e)
    {
        if (GameGlobeData.IsGamePaused) GameGlobeData.IsGamePaused = false;
        else GameGlobeData.IsGamePaused = true;
    }

    private void GameCon_OnGamePaused(object sender, System.EventArgs e)
    {
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
}
