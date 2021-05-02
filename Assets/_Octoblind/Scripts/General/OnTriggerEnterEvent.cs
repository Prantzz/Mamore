using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnterEvent : MonoBehaviour
{

    [SerializeField] private UnityEvent onTriggerEnter;
    [SerializeField] private UnityEvent onTriggerExit;

    [SerializeField] private string tagToCollide;
    [SerializeField] private string type;


    private void Update()
    {
        Debug.Log("Habilitado");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (type != null)
        {
            if (other.GetComponentInParent<objectController>()?.type == this.type)
                onTriggerEnter?.Invoke();
        }
        else if (tagToCollide != null)
        {
            if (other.CompareTag(tagToCollide))
            {
                Debug.Log("Ya");
                GetComponentInParent<Puzzle1>().DesajustarDegrau();
            }
                
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (type != null)
        {
            if (other.GetComponentInParent<objectController>()?.type == this.type)
                onTriggerExit?.Invoke();
        }
        else if (tagToCollide != null)
        {
            
               
        }

    }

    public void ResetTrigger()
    {
        type = null;
    }

}
