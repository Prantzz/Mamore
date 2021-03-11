using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public CharacterController CharCon;
    private float movX, movZ, speed = 0.1f, gravity = -19.81f, jumpSpeed = 8f;
    readonly float speedConst = 0.1f;
    public Vector3 xzMove, yMove;
    public bool isSprinting = false;
    public static bool isCrouching = false;
    public UnityEvent OnSpacePressed, OnShiftKeepPressed, OnShiftReleased, OnCtrlKeepPressed, OnCtrlReleased;
    void Start()
    {
        CharCon = GetComponent<CharacterController>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) OnSpacePressed?.Invoke();
        if (Input.GetKey(KeyCode.LeftShift)) OnShiftKeepPressed?.Invoke();
        if (Input.GetKeyUp(KeyCode.LeftShift)) OnShiftReleased?.Invoke();
        if (Input.GetKey(KeyCode.LeftControl)) OnCtrlKeepPressed?.Invoke();
        if (Input.GetKeyUp(KeyCode.LeftControl)) OnCtrlReleased?.Invoke();
    }
    void FixedUpdate()
    {
        MoveInAxis();
        Gravity();
    }
    private void MoveInAxis()
    {
        #region SPRINTING_&_CROUCHING_SPEED_LOGIC
        if (isSprinting && !isCrouching) speed = speedConst * 2;
        else if (isCrouching && !isSprinting) speed = speedConst * 0.5f;
        else if (isCrouching && isSprinting) speed = speedConst * 0.8f;
        else speed = speedConst;
        #endregion


        movX = Input.GetAxisRaw("Horizontal");
        movZ = Input.GetAxisRaw("Vertical");
        xzMove = (transform.right * movX + transform.forward * movZ).normalized;

        CharCon.Move(xzMove * speed);
    }
    private void Gravity()
    {
        yMove.y += gravity * Time.deltaTime;
        CharCon.Move(yMove * Time.deltaTime);
        if (CharCon.isGrounded && yMove.y < 0) yMove.y = 0; 
    }
    #region EVENTS_METHODS
    public void Jump() 
    {
        if (CharCon.isGrounded)
        {
            yMove.y = jumpSpeed;            
        }
    }
    public void Sprint() 
    {
        isSprinting = true;
    }
    public void Walk()
    {
        isSprinting = false;
    }
    public void Crouch() 
    {
        isCrouching = true;
    }
    public void Stand() 
    {
        isCrouching = false;
    }
    #endregion
}
