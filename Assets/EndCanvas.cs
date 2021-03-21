using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCanvas : MonoBehaviour
{
    public void Voltar()
    {
        GameGlobeData.IsGameOver = false;
        GameGlobeData.isCompassCollected = false;
        GameGlobeData.IsDocumentCollected = false;
        GameGlobeData.SceneHasEnded = true;
    }

    public void Sair() 
    {
        Application.Quit();
    }

}



