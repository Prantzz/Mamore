using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndCanvas : MonoBehaviour
{
    public Image fade;
    public Transform good;
    public Transform bad;
    public Transform ending;
    public float scrollSpeed;
    public float fadeSpeed;
    public Image octo;
    public Text producao;
    public bool continueCredits = false;
    public bool switcher;
    public Text memo;
    private void Start()
    {
        ending = good;
        StartCoroutine("Main");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && switcher && continueCredits == true)
        {
            switcher = false;
            continueCredits = false;
        }
        if (Input.GetMouseButtonDown(0) && switcher && continueCredits == false) continueCredits = !continueCredits;
        
    }
    IEnumerator Main()
    {
        Text eC = ending.GetComponent<Text>();
        yield return new WaitForSecondsRealtime(1f);
        while (fade.color.a > 0)
        {
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, fade.color.a - fadeSpeed);

            yield return new WaitForSecondsRealtime(0.01f);
        }
        while (ending.position.y < 0)
        {
            ending.position = new Vector3(ending.position.x,ending.position.y+ scrollSpeed,ending.position.z);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        Debug.Log("Esperando 2 segundos");
        yield return new WaitForSecondsRealtime(1f);
        Debug.Log("Terminei de esperar");
        while (continueCredits == false)
        {
            if (!switcher) switcher = true;
            yield return new WaitForEndOfFrame();
        }
        while (eC.color.a > 0)
        {
            eC.color = new Color(eC.color.r, eC.color.g, eC.color.b, eC.color.a - (fadeSpeed*3));            
            yield return new WaitForSecondsRealtime(0.01f);
        }
        while(producao.color.a < 1)
        {
            producao.color = new Color(producao.color.r, producao.color.g, producao.color.b, producao.color.a + (fadeSpeed * 3));
            octo.color = new Color(octo.color.r, octo.color.g, octo.color.b, octo.color.a + (fadeSpeed*3));
            yield return new WaitForSecondsRealtime(0.01f);
        }
        yield return new WaitForSecondsRealtime(1f);
        while (memo.color.a < 1)
        {
            memo.color = new Color(memo.color.r, memo.color.g, memo.color.b, memo.color.a + (fadeSpeed*3));
            yield return new WaitForSecondsRealtime(0.01f);
        }
        yield return new WaitForSecondsRealtime(1f);
        while (continueCredits == true)
        {
            switcher = false;
            continueCredits = false;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSecondsRealtime(1f);
        while(continueCredits == false && switcher == false && memo.color.a > 0)
        {
            memo.color = new Color(memo.color.r, memo.color.g, memo.color.b, memo.color.a - (fadeSpeed * 3));
            yield return new WaitForSecondsRealtime(0.01f);
        }
        yield return null;
    }

    /* Código antigo do Duxo, deixei aqui, é nois.
    private void Start()
    {
        GameGlobeData.IsCamLock = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Voltar()
    {
        GameGlobeData.IsGameOver = false;
        GameGlobeData.isCompassCollected = false;
        GameGlobeData.IsDocumentCollected = false;
        SceneManager.LoadScene(1);
    }

    public void Sair() 
    {
        Application.Quit();
    }
    */

}



