using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallIfNotHammered : MonoBehaviour
{
    //aqui só para eu me recordar do polimorfismo
    //dependendo do objeto filho dentro dessa variável de tipo puzzle, se o método desajustar parte for sobreescrito, o método do filho será chamado
    //mesmo sendo do tipo da classe do pai (só funciona pra métodos virtuais da classe pai)
    public Puzzle puzzle;

    private void Start()
    {
        
        puzzle = GetComponentInParent<Puzzle>();
        transform.SetParent(null);
    }

    public void PlayerCollided()
    {
        if (puzzle == null)
            return;
        puzzle.DesajustarParte(this.gameObject);
    }

}
