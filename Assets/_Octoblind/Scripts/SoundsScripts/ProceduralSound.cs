using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ProceduralSound : MonoBehaviour
{
    //Infelizmente a forma como eu fiz isso não permite tocar sons em diferentes alturas dependendo da espécie do animal...
    //Isso algum dia deveria ser arrumado :(
    [Header("HARD CODED 4 e 5!!!")]
    public AudioManager AU;
    public GameObject Instance;
    public Transform PlayerPos;
    private int[] ProceduralArr;
    public void SetUp()
    {
        int size = 0;
        foreach (AudioGroup a in AU.AudioGroupes)
        {
            //HARD CODED
            if (a.Type <= 3) continue;
            foreach (AudioClip c in a.Sounds)
            {
                size += 1;
            }
        }
        ProceduralArr = new int[size];
    }
    void InvokeSound()
    {
        //Escolhe um som
        int SoundIndex = FindValidSound();

        //Converte posição
        KeyValuePair<int,int> SoundToPlay = FindPos(SoundIndex);

        //Escolhe um lugar aleatório para tocar o som e toca.
        AU.PullSound(PickRandomPos(), SoundToPlay.Key, SoundToPlay.Value);

        //Modifica o array procedural
        ProceduralArr[SoundIndex] += 1;
    }
    int FindValidSound()
    {
        List<int> Remeaming = new List<int>();
        int min = ProceduralArr.Min();
        //Verifica se o som pode ser tocado
        for (int i = 0; i < ProceduralArr.Length; i++)
        {
            if (ProceduralArr[i] > min) continue;
            else Remeaming.Add(i);
        }
        return Remeaming[UnityEngine.Random.Range(0, Remeaming.Count)];
    }
    Vector3 PickRandomPos()
    {
        return Vector3.back;
    }
    KeyValuePair<int, int> FindPos(int numb)
    {
        KeyValuePair<int, int> toReturn;
        //HARD CODED NUMBERS ME NO LIKE!
        //Idealmente fazei um sistema pra detectar q quantidade de sons que estão nos slots de animas porém não tenho tempo pra isso e não é tão essencial.
        if (numb < AU.AudioGroupes[4].Sounds.Count) toReturn = new KeyValuePair<int, int>(4,numb);
        else toReturn = new KeyValuePair<int, int>(5, numb- AU.AudioGroupes[4].Sounds.Count);
        return toReturn;
    }
}
