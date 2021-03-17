using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MenuHandler : MonoBehaviour
{
    public VolumeProfile MenuVol;
    public GameObject MainMenuPanel, CreditsPanel;
    public Image MainLogo;
    private AudioManager AU;
    DepthOfField Dof;

    private void Start()
    {
        AU = GameObject.Find("GameGlobeData").GetComponent<AudioManager>();
        CreditsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);

        DepthOfField tmp;
        if (MenuVol.TryGet(out tmp))
        {
            Dof = tmp;
        }
    }

    public void Credits() 
    {
        AU.PullSound(Camera.main.transform.position, 1, 1);
        MainLogo.rectTransform.position = new Vector3(1920f/2, 950f, MainLogo.rectTransform.position.z);

        Dof.focusDistance.value = 0f;
        

        CreditsPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }
    public void Voltar()
    {
        AU.PullSound(Camera.main.transform.position, 1, 1);
        MainLogo.rectTransform.position = new Vector3(1920f/4, 850f, MainLogo.rectTransform.position.z);

        Dof.focusDistance.value = 0.9f;


        CreditsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }
}
