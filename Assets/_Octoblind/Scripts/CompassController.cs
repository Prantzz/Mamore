using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassController : MonoBehaviour
{
    public GameObject iconPrefab;
    List<QuestHandler> QuestList = new List<QuestHandler>();

    private RawImage compass;
    private Transform player;

    float compassUnit;

    public QuestHandler test;

    private void Start()
    {
        
        compass = GetComponent<RawImage>();
        player = GameObject.Find("Player").transform;
        compassUnit = compass.rectTransform.rect.width / 360;

        AddQuestMarker(test);

    }
    private void Update()
    {
        compass.uvRect = new Rect(player.localEulerAngles.y / 360f, 0f, 1f, 1f);

        foreach(QuestHandler marker in QuestList)
        {
            marker.thisImg.rectTransform.anchoredPosition = GetPosOnCompass(marker);
        }
    }

    public void AddQuestMarker(QuestHandler marker)
    {
        GameObject newMarker = Instantiate(iconPrefab, compass.transform);
        marker.thisImg = newMarker.GetComponent<Image>();
        marker.thisImg.sprite = marker.QuestMark;

        QuestList.Add(marker);
    }

    Vector2 GetPosOnCompass (QuestHandler marker) 
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 playerFwd = new Vector2(player.transform.forward.x, player.forward.z);

        float angle = Vector2.SignedAngle(marker.position - playerPos, playerFwd);

        return new Vector2(compassUnit * angle, 0f);
    }
}
