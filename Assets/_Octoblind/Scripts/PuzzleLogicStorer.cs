using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLogicStorer : MonoBehaviour
{
    public static bool[][] StateStorer;
    void Start()
    {
        /*Puzzle 0 = Dormentes dos Trilhos (Ações necessárias=> Colocar os dormentes no lugar e martela-los)
         *Puzzle 1 = ...
         */
        StateStorer = new bool[][]
        {
            new bool[]{false,false,false,false}
        };
    }
    public static bool CheckState(int Puzzle)
    {
        foreach(bool b in StateStorer[Puzzle])
        {
            if (!b) return false;
        }
        return true;
    }
    public static void ChangeState(int puzzle, int piece, bool state)
    {
        StateStorer[puzzle][piece] = state;
    }
}
