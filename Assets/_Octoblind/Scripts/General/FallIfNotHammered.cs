using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallIfNotHammered : MonoBehaviour
{
    [SerializeField] Puzzle1 puzzle1;
    
    public void PlayerCollided()
    {
        Debug.Log("chamou o desajustar");
        puzzle1.DesajustarDegrau(this.gameObject);
    }

}
