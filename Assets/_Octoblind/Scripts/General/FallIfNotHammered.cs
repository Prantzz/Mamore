using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallIfNotHammered : MonoBehaviour
{
    [SerializeField] Puzzle puzzle;

    public void PlayerCollided()
    {
        //aqui s� para eu me recordar
        //dependendo do objeto filho, o m�todo espec�fico dele ser� chamado, se sobreescrever o m�todo pai
        puzzle.DesajustarParte(this.gameObject);
    }

}
