using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCanvas : MonoBehaviour
{
    private void Start()
    {
        GameGlobeData.IsCamLock = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Voltar()
    {
        GameGlobeData.IsGameOver = false;
        GameGlobeData.isCompassCollected = false;
        GameGlobeData.IsDocumentCollected = false;
        SceneManager.LoadScene(1);
    }

    public void Sair() 
    {
        Application.Quit();
    }

}



