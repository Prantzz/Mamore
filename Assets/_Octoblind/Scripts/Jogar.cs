using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogar : MonoBehaviour
{
    private AudioManager AU;
    private void Start()
    {
        AU = GameObject.Find("GameGlobeData").GetComponent<AudioManager>();
    }
    public void JogarBut() 
    {
        AU.PullSound(Camera.main.transform.position, 1, 0);
        GameGlobeData.SceneHasEnded = true;
    }
}
