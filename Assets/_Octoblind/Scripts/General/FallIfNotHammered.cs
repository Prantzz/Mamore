using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallIfNotHammered : MonoBehaviour
{
    [SerializeField] Puzzle puzzle;

    public void PlayerCollided()
    {
        //aqui só para eu me recordar
        //dependendo do objeto filho, o método específico dele será chamado, se sobreescrever o método pai
        puzzle.DesajustarParte(this.gameObject);
    }

}
