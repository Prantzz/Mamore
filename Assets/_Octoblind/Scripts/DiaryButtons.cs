using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaryButtons : MonoBehaviour
{
    public string Title;
    public string Body;

    public Text CanvasTitle, CanvasBody;

    private Button thisBut;

    private void Start()
    {
        thisBut = gameObject.GetComponent<Button>();
        CanvasTitle = GameObject.Find("TituloDiario").GetComponent<Text>();
        CanvasBody = GameObject.Find("CorpoDiario").GetComponent<Text>();
    }

    private void Update()
    {
        gameObject.transform.GetChild(0).GetComponent<Text>().text = Title;
        thisBut.onClick.AddListener(UpdateLog);
    }

    public void UpdateLog() 
    {
        CanvasTitle.text = Title;
        CanvasBody.text = Body;
    }

}
