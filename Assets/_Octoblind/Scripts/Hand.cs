using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public InventoryObject inventory;
    public int SelectInt;
    public static string HOLDING_TOOL;
    
    void Update()
    {
        UpdateSlot();
        if (inventory.Container.Count != 0) 
        {
            if(inventory.Container[SelectInt] != null) 
            {
                if (gameObject.transform.childCount <= 0)
                {
                    if (inventory.Container[SelectInt].item != null)
                    {
                        Instantiate(inventory.Container[SelectInt].item.prefab, gameObject.transform);
                        string toolName = inventory.Container[SelectInt].item.ToolName;
                        HOLDING_TOOL = toolName;
                    }
                }

               
            }
            
        }
        
    }


    public void UpdateSlot() 
    {
        if (inventory.Container.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SelectInt = 0;
                if (gameObject.transform.childCount >= 1) Destroy(gameObject.transform.GetChild(0).gameObject);
            }
            if (inventory.Container.Count > 1)
            {
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    SelectInt = 1;
                    if (gameObject.transform.childCount >= 1) Destroy(gameObject.transform.GetChild(0).gameObject);
                }
                if (inventory.Container.Count > 2)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        SelectInt = 2;
                        if (gameObject.transform.childCount >= 1) Destroy(gameObject.transform.GetChild(0).gameObject);
                    }
                    if (inventory.Container.Count > 3)
                    {
                        if (Input.GetKeyDown(KeyCode.Alpha4))
                        {
                            SelectInt = 3;
                            if (gameObject.transform.childCount >= 1) Destroy(gameObject.transform.GetChild(0).gameObject);
                        }
                    }
                }
            }
        }
    }
}
