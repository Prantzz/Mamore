using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestCanvasLogic : MonoBehaviour
{
    public GameObject questBanner;
    public Text bannerText;


    private void Start()
    {
        questBanner.SetActive(false);
    }

    public void changeTextAndActive(string text, bool active) 
    {
        bannerText.text = text;
        questBanner.SetActive(active);
    }
}
