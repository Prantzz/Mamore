using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnterEvent : MonoBehaviour
{

    [SerializeField] private UnityEvent onTriggerEnter;
    [SerializeField] private UnityEvent onTriggerExit;

    [Header("Apenas preencher apenasmente um dos dois apenas")]
    [SerializeField] private string tagToCollide;
    [SerializeField] private string type;

    private void OnTriggerEnter(Collider other)
    {
        if (type != null)
        {
            if (other.GetComponentInParent<objectController>()?.type == this.type)
                onTriggerEnter?.Invoke();
        }
        else if (tagToCollide != null)
        {
            if (other.CompareTag("Player"))
                onTriggerEnter?.Invoke();
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
            if (other.CompareTag("Player"))
                onTriggerExit?.Invoke();
        }

    }
}
