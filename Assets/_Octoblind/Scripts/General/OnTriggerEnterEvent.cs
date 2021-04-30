using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnterEvent : MonoBehaviour
{

    [SerializeField] private UnityEvent someEvent;
    [SerializeField] private string tagToCollide;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag(tagToCollide))
            someEvent?.Invoke();

    }

}
