using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaryManager : MonoBehaviour
{
    private List<DocumentObject> Logs = new List<DocumentObject>();
    private readonly int DocumentsToGoodEnding = 6;
    private void Update()
    {
        if (Logs.Count >= DocumentsToGoodEnding) 
        {
            GameGlobeData.IsGoodEnding = true;
        }
        //Debug.Log("numero de docs coletados e: " + Logs.Count);
       // Debug.Log("o titulo do primeiro e: " + Logs[0].Title);
       // Debug.Log("o titulo do segundo e: " + Logs[1].Title);
    }

    public void addDocumentToList(DocumentObject document) 
    {
        if (Logs.Contains(document)) 
        {
            return;
        }
        else 
        {
            Logs.Add(document);
        }
    }
}
