using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PhotoCon : MonoBehaviour
{
    public Image photoImage;
    public GameObject PhotoPanel;

    private void Update()
    {
        PhotoPanel.SetActive(GameGlobeData.IsPhotoCollected);
    }

    public void ChangePhoto(Sprite sprite) 
    {
        photoImage.sprite = sprite;
    }
}
