using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static float INSANITY;
    public string CurupiraTag = "Curupira";
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
                INSANITY += 6f * Time.deltaTime;
            }
        }
        if (INSANITY >= 15f)
        {
            INSANITY -= 2f * Time.deltaTime;
        }
        Debug.Log(INSANITY);
    }
}
