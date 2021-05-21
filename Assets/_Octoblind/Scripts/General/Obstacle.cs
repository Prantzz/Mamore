using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private string CorrectTool;
    [SerializeField] private float dissolveVelocity = 1f;
    [SerializeField] private float timer = 6f;
    [SerializeField] private bool dissolveGroup = true;

    private MeshRenderer[] childRenderers;
    private MeshRenderer childRenderer;
    public string correctTool { get => CorrectTool; }

    private bool m_remove;

    

    private void Start()
    {
        m_remove = false;

        //pega meshrenderers das children
        if (dissolveGroup)
        {
            childRenderers = GetComponentsInChildren<MeshRenderer>();
            //torna cada material unico pra não cagar o material de outros obstaculos ao mesmo tempo (que gambiarra tenho que fazer por vc hein unity)
            for (int i = 0; i < transform.childCount - 1; i++)
            {
                childRenderers[i].material = new Material(childRenderers[i].material);
            }
        }
        else
        {
            childRenderer = GetComponent<MeshRenderer>();
            childRenderer.material = new Material(childRenderer.material);
        }
            
        
        
    }

    //método chamado pelo raycast da nossa querida serra
    public void Remove() => m_remove = true;

    private void Update()
    {
        if (m_remove)
        {
            //timer pra dissolver nossos queridos obstaculos
            timer -= dissolveVelocity * Time.deltaTime;
            if (timer > 0f)
            {
                foreach (var a in childRenderers)
                {
                   
                    a.material.SetFloat("_CutoffHeight", timer);
                }
            }
            //dissolveu acabou
            else
            {
                gameObject.SetActive(false);
            }  
        } 
        
    }


}
