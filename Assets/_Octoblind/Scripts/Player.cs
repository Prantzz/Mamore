using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static float INSANITY;
    public string CurupiraTag = "Curupira";
    public InventoryObject inventory;
    void Start()
    {
        INSANITY = 0f;    
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 15f))
        {
            Transform _curupira = hit.transform;
            if (_curupira.CompareTag(CurupiraTag)) 
            {
                INSANITY += 30f * Time.deltaTime;
            }
        }
        if (INSANITY >= 0f)
        {
            INSANITY -= 20f * Time.deltaTime;
        }
        if (INSANITY >= 100f) 
        {
            INSANITY = 100f;
            EndGame();
        }
        //Debug.Log(INSANITY);
    }


    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }

    public void EndGame() 
    {
        GameGlobeData.IsGameOver = true;
    }
}
