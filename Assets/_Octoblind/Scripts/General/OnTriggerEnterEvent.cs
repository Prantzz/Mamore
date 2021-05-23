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
    private Puzzle1 puzzle1;
    public GameObject tabua;

    bool disabled = false;

    private void Start()
    {
        puzzle1 = GetComponentInParent<Puzzle1>();
    }

    private void Update()
    {
        //Debug.Log("Habilitado");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (type != null && !disabled) 
        {
            
            if (other.GetComponentInParent<objectController>()?.type == this.type)
                onTriggerEnter?.Invoke();
        }
        else if (tagToCollide != null)
        {
            if (other.CompareTag(tagToCollide))
            {
               
                puzzle1.DesajustarDegrau();
            }
                
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (type != null && !disabled)
        {
            if (other.GetComponentInParent<objectController>()?.type == this.type)
                onTriggerExit?.Invoke();
        }
        else if (tagToCollide != null)
        {
            
               
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
