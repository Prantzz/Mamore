using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarroDeEmpurrarScript : MonoBehaviour
{
    private bool sound;
    private bool canMove;
    public Animator ownAnim;
    public Puzzle puzzle1;
    public Puzzle puzzle2;
    public float velocity;


    public void Move()
    {
        if (canMove)
        {
            GameGlobeData.AU.PullSound(this.transform.position, 8, sound ? 0 : 1);
            sound = !sound;
            canMove = false;
            ownAnim.SetBool("Main" ,sound);
            velocity += 0.12f;
        }
        
    }
    private void Update()
    {
        //Em raras situa��es o carrinho buga e faz muiuto barulho
        canMove = true;
        if (transform.position.x >= 89.5f && !puzzle1.CheckStep(0))
        {
            GameGlobeData.AU.PullSound(this.transform.position, 8, 2);
            velocity = -velocity;
        }
        if (transform.position.x >= 101.5f && !puzzle1.CheckStep(3))
        {
            GameGlobeData.AU.PullSound(this.transform.position, 8, 2);
            velocity = -velocity;
        }
        if (velocity > 0)
        {
            transform.position = transform.position + Vector3.right * velocity;
            velocity /= 1.02f;
            if (velocity < 0.0001f) velocity = 0;
        }
        else if(velocity <0)
        {
            transform.position = transform.position + Vector3.left * -velocity;
            velocity /= 1.02f;
            if (velocity > -0.0001f) velocity = 0;
        };
        
    }
}