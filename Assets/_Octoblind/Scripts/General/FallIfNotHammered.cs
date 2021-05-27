using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallIfNotHammered : MonoBehaviour
{
    //aqui s� para eu me recordar do polimorfismo
    //dependendo do objeto filho dentro dessa vari�vel de tipo puzzle, se o m�todo desajustar parte for sobreescrito, o m�todo do filho ser� chamado
    //mesmo sendo do tipo da classe do pai (s� funciona pra m�todos virtuais da classe pai)
    [SerializeField] Puzzle puzzle;

    public void PlayerCollided()
    {
        if (puzzle == null)
            return;
        puzzle.DesajustarParte(this.gameObject);
    }

}
