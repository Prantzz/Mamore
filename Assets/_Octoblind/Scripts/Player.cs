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
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Transform _curupira = hit.transform;
            if (_curupira.CompareTag(CurupiraTag))
            {
                INSANITY += 75f * Time.deltaTime;
            }
        }
        if (INSANITY >= 15f)
        {
            INSANITY -= 2f * Time.deltaTime;
        }
        if (INSANITY >= 100f) 
        {
            INSANITY = 0f;
            GameGlobeData.SceneHasEnded = true;

        }
        Debug.Log(INSANITY);
    }


    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }

}
