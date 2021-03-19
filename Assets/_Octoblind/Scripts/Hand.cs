using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public InventoryObject inventory;
    public int SelectInt;
    
    void Update()
    {
        UpdateSlot();
        if (inventory.Container.Count != 0) 
        {
            if(inventory.Container[SelectInt] != null) 
            {
                if(gameObject.transform.childCount <= 0)
                Instantiate(inventory.Container[0].item.prefab, gameObject.transform);
            }
            
        }
        
    }


    public void UpdateSlot() 
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            SelectInt = 0;
            if (gameObject.transform.childCount >= 1) Destroy(gameObject.transform.GetChild(0).gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectInt = 1;
            if (gameObject.transform.childCount >= 1) Destroy(gameObject.transform.GetChild(0).gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectInt = 2;
            if (gameObject.transform.childCount >= 1) Destroy(gameObject.transform.GetChild(0).gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectInt = 3;
            if (gameObject.transform.childCount >= 1) Destroy(gameObject.transform.GetChild(0).gameObject);
        }
    }
}
