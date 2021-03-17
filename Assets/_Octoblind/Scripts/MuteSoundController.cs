using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteSoundController : MonoBehaviour
{
    public Sprite Swap1;
    public Sprite Swap2;
    private Image Image;
    void Start()
    {
        Image = GetComponent<Image>();
    }
    public void Switch()
    {
        AudioListener.volume = AudioListener.volume <= 0 ? 1 : 0;
        Swap2 = Image.sprite;
        Image.sprite = Swap1;
        Swap1 = Swap2;
    }
}
