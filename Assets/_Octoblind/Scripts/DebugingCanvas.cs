using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugingCanvas : MonoBehaviour
{
    public int behaviour;
    private Text t;
    void Start()
    {
        t = GetComponent<Text>();
        if (behaviour == 0) t.text = "Build: " + Application.version;
    }

    void Update()
    {
        if (behaviour == 1) t.text = "FPS: " + 1.0f / Time.deltaTime;
    }
}
