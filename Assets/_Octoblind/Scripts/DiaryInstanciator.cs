using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryInstanciator : MonoBehaviour
{
    [SerializeField]
    private InventoryDocument inventory;

    public GameObject prefabToSpawn;

    public GameObject DiaryPanel; 

    private DiaryButtons Button;
    private RectTransform OwnRect;
    private int Y_DISTANCE = 80;

    private void Update()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (DiaryPanel.transform.childCount <= 3 + i)
            {
                Button = prefabToSpawn.GetComponent<DiaryButtons>();
                OwnRect = prefabToSpawn.GetComponent<RectTransform>();
                Button.Title = inventory.Container[i].item.Title;
                Button.Body = inventory.Container[i].item.Body;
                OwnRect.anchoredPosition = new Vector3(OwnRect.anchoredPosition.x, OwnRect.anchoredPosition.y - (Y_DISTANCE * i));
                Instantiate(prefabToSpawn, DiaryPanel.transform);
                break;
            }
        }
    }
}
