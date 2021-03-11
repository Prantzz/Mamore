using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    private Image thisImg;
    private GameGlobeData GameCon;
    float fadeSpeed;

    void Start()
    {
        thisImg = GetComponent<Image>();
        
        if (gameObject.tag == "Out") fadeSpeed = 0f;
        else fadeSpeed = 1f;

        GameCon = GameObject.Find("GameGlobeData").GetComponent<GameGlobeData>();
        GameCon.OnSceneStart += GameCon_OnSceneStart;
        GameCon.OnSceneEnd += GameCon_OnSceneEnd;
    }

    private void GameCon_OnSceneEnd(object sender, System.EventArgs e)
    {
        GameCon.OnSceneStart -= GameCon_OnSceneStart;
        if (gameObject.tag == "Out")
        {
            gameObject.SetActive(true);
            fadeSpeed += 0.8f * Time.deltaTime;
            thisImg.color = new Color(thisImg.color.r, thisImg.color.g, thisImg.color.b, fadeSpeed);
            if (fadeSpeed >= 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                GameGlobeData.SceneHasEnded = false;
                GameCon.OnSceneEnd -= GameCon_OnSceneEnd;
                return;
            }
        }
        else return;
    }

    private void GameCon_OnSceneStart(object sender, System.EventArgs e)
    {
        if (gameObject.tag == "In")
            {
                fadeSpeed -= 0.8f * Time.deltaTime;
                thisImg.color = new Color(thisImg.color.r, thisImg.color.g, thisImg.color.b, fadeSpeed);
                if (fadeSpeed <= 0)
                {
                    gameObject.SetActive(false);
                    fadeSpeed = 0;
                    return;
                }
            }
            else if (gameObject.tag == "Out") gameObject.SetActive(false);
            else return;
    }
}
