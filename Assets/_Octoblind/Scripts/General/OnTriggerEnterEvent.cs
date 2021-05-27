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

    public bool disabled = false;

    private void Start()
    {
        //  DON'T DELETE THIS METHOD ETHAN, WE NEED OUR CHECKBOX HERE
    }

    // "NO MORE CURLY BRACKETS" - OSBOURNE, Ozzy. 1991.
    private void OnTriggerEnter(Collider other)
    {
        if (disabled)
            return;

        if (other.GetComponentInParent<objectController>() == null)
            return;

        if (other.GetComponentInParent<objectController>().type == this.type)
            onTriggerEnter?.Invoke();
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (disabled)
            return;

        if (other.GetComponentInParent<objectController>() == null)
            return;

        if (other.GetComponentInParent<objectController>().type == this.type)
            onTriggerExit?.Invoke();
    }

    public void AlternateTrigger(bool state) => disabled = state;

    public void DisableTrigger() => GetComponent<BoxCollider>().enabled = false;


}
