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
        //Debug.Log(HOLDING_TOOL);
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
    public void ActiveItem()
    {
        //Caso esteja segunrando uma ferramenta
        if (!string.IsNullOrEmpty(HOLDING_TOOL))
        {
            //Cast de um ray do mouse ao infinito e além
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Caso acerte Out num RaycastHit
            if (Physics.Raycast(ray, out RaycastHit hit,2f))
            {
                //The hit is my bitch now
                Transform hitT = hit.transform;
                
                //Caso acerte um PuzzleCollider
                if (hitT.CompareTag("PuzzleCollider"))
                {
                    SimplePuzzleCollider SPC = hitT.GetComponent<SimplePuzzleCollider>();
                    //Caso ele esteja usando a ferramenta correta para o serviço
                    if (string.Equals(SPC.correctTool, HOLDING_TOOL))
                    {
                        //Avance um step do Puzzle
                        SPC.TryToAchiveStep();
                    }
                    //Caso ele não esteja usando a fita certa
                    else
                    {
                        //Tome dano pra parar de ser otário
                        transform.parent.transform.parent.transform.parent.GetComponent<PlayerController>().TakeDamage();
                    }
                }
                //Caso contrário não faça nada
                else{}
            }
        }        
    }
    
}
