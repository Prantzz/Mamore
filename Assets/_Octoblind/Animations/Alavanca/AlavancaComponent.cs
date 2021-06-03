using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlavancaComponent : MonoBehaviour
{
    public Animator owmAnim;
    public AudioSource AU;
    public Puzzle7 p7;
    public int x;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void Switch()
    {
        owmAnim.SetBool("ForB", !owmAnim.GetBool("ForB"));
        AU.Play();
        p7.AchiveStep(x,owmAnim.GetBool("ForB"));
    }
}
