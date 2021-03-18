using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocumentManager : MonoBehaviour
{
    public Text DocTitle, DocBody;
    private string _DocTitle, _DocBody;
    public GameObject ownCanvas;

    private void Update()
    {
        DocTitle.text = _DocTitle;
        DocBody.text = _DocBody;
        if (GameGlobeData.IsDocumentCollected) 
        {
            ownCanvas.SetActive(true);
            GameGlobeData.IsCamLock = true;
            GameGlobeData.FreezeGame();
        }
        else
        {
            if (!GameGlobeData.IsGamePaused)
            {
                ownCanvas.SetActive(false);
                GameGlobeData.IsCamLock = false;
                GameGlobeData.UnfreezeGame();
            }

        }

    }

    public void ChangeDocument(string title, string body)
        {
            _DocTitle = title;
            _DocBody = body;
        }
}
