using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SplashCon : MonoBehaviour
{
    private VideoPlayer thisVP;

    private void Start()
    {
        thisVP = GetComponent<VideoPlayer>();
    }
    private void Update()
    {
        if (thisVP.isPaused) SceneManager.LoadScene(1);
    }
}
