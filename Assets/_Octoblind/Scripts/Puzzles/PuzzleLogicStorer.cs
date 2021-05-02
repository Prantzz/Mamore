using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleLogicStorer : MonoBehaviour
{
    public static Puzzle[] listOfPuzzles;
    private void Start()
    {
        SceneManager.activeSceneChanged += GetAllPuzzles;
    }
    private void GetAllPuzzles(Scene cur, Scene next)
    {
        //Caso seja a cena principal
        if (next.buildIndex == 2)
        {
            listOfPuzzles = GameObject.FindObjectsOfType<Puzzle>();
        }
    }
    public static bool CheckForBestEnding()
    {
        foreach(Puzzle p in listOfPuzzles)
        {
            if (!p.completed) return false;
        }
        return true;
    }

}
