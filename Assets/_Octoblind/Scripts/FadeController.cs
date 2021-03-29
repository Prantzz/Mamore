using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    void Awake() 
    {
        if (gameObject.tag == "In") gameObject.SetActive(true);
    }
}
