using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameGlobeData : MonoBehaviour
{
    public event EventHandler OnSceneStart;
    public event EventHandler OnSceneEnd;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameResumed;
    public event EventHandler OnEscPressed;
    public static bool SceneHasEnded = false;
    public static bool IsGamePaused = false;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().isLoaded) OnSceneStart?.Invoke(this, EventArgs.Empty);
        if (SceneHasEnded) OnSceneEnd?.Invoke(this, EventArgs.Empty);
        if (IsGamePaused) OnGamePaused?.Invoke(this, EventArgs.Empty);
        if (!IsGamePaused) OnGameResumed?.Invoke(this, EventArgs.Empty);
        if (Input.GetKeyDown(KeyCode.Escape)) OnEscPressed?.Invoke(this, EventArgs.Empty);
        if (Input.GetKeyDown(KeyCode.Z)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);
        if (Input.GetKeyDown(KeyCode.C)) this.GetComponent<AudioManager>().PullSound(this.transform.position, 0, 0);
    }
}
