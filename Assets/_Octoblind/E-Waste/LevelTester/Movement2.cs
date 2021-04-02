using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;

    [SerializeField]
    float gravity = 9.81f;

    CharacterController cc;

    Vector3 velocity;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    float groundDistance = 0.4f;

    [SerializeField]
    LayerMask groundMask;

    bool isGrounded;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

   
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        cc.Move(move * speed * Time.deltaTime);

        if (isGrounded)
        {
            velocity.y = -2f;
        }
        else
        {
            velocity.y -= gravity;

            cc.Move(velocity * Time.deltaTime);
        }

       
    }
}
