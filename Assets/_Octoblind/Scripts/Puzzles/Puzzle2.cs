using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed public class Puzzle2 : Puzzle
{
    //Puzzle da mesa

    [SerializeField] private Transform finalPosPemesa0;
    [SerializeField] private Transform finalPosPemesa1;
    [SerializeField] private Transform finalPosPemesa2;
    [SerializeField] private Transform finalPosPemesa3;

    private Transform Encaixe0;
    private Transform Encaixe1;
    private Transform Encaixe2;
    private Transform Encaixe3;
    private int a = 0; 

    bool locker1 = false;
    bool locker2 = false;

    private void Start()
    {
        Encaixe0 = transform.GetChild(0);
        Encaixe1 = transform.GetChild(1);
        Encaixe2 = transform.GetChild(2);
        Encaixe3 = transform.GetChild(3);
    }

    public override void MiddleStep()
    {
        
    }

    private void AjustarPemesa()
    {

    }

    public override void DesajustarParte(GameObject peMesa)
    {
     
    }

}
