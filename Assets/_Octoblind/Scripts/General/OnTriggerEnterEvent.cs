using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnterEvent : MonoBehaviour
{

    [SerializeField] private UnityEvent onTriggerEnter;
    [SerializeField] private UnityEvent onTriggerExit;
    public int triggerIndex;

    [SerializeField] private string tagToCollide;
    [SerializeField] private string type;

    bool disabled = false;
   

    private void Update()
    {
        //Debug.Log("Habilitado");
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (type != null && !disabled) 
        {
            if (other.GetComponentInParent<objectController>()!= null)
            {
                if (other.GetComponentInParent<objectController>().type == this.type)
                {
                    onTriggerEnter?.Invoke();
                }
            }
               
               
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (type != null && !disabled)
        {
            if (other.GetComponentInParent<objectController>() != null)
            {
                if (other.GetComponentInParent<objectController>().type == this.type)
                {
                    onTriggerExit?.Invoke();
                }
            }

        }

    }

    public void AlternateTrigger(bool state)
    {
        disabled = state; 
    }

    public void DisableTrigger()
    {
        this.enabled = false;
    }

}
