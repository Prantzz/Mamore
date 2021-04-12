using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [Range(1,100)]
    public int behaviour;
    private void OnTriggerEnter(Collider other)
    {
        //Caso o jogador colida com o trigger mande o valor do behaviour desse objetos para o canvas
        GameGlobeData.GameCon.TutorialTrigger(behaviour, true);
        GameGlobeData.GameCon.GetComponent<AudioManager>().PullSound(this.transform.position, 1, 1);
        Destroy(this);
    }
}
