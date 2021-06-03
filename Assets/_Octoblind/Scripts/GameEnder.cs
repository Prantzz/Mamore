using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("spdofjikspd");
        if (other.gameObject.CompareTag("Carro"))
        {
            GameGlobeData.SceneHasEnded = true;
        }
    }
}
