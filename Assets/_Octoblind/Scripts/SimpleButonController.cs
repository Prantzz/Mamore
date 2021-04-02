using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleButonController : MonoBehaviour
{
    public Sprite Swap1;
    public Sprite Swap2;
    private Image Image;
    void Start()
    {
        Image = GetComponent<Image>();
    }
    public void SwitchPS()
    {
        if (GameGlobeData.PSList[0].main.maxParticles!=0)
        {
            foreach(ParticleSystem P in GameGlobeData.PSList)
            {
                var main = P.main;
                main.maxParticles = 0;
            }
        }
        else
        {
            foreach (ParticleSystem P in GameGlobeData.PSList)
            {
                var main = P.main;
                main.maxParticles = 1000;
            }
        }
        ImageSwap();
    }
    public void SwitchSound()
    {
        AudioListener.volume = AudioListener.volume <= 0 ? 1 : 0;
        ImageSwap();        
    }
    private void ImageSwap()
    {
        Swap2 = Image.sprite;
        Image.sprite = Swap1;
        Swap1 = Swap2;
    }
}
