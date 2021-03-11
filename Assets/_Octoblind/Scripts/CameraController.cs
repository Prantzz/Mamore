using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /* 
     olá eu sou um comentário de teste, apague-me assim que me ver
     ou não me apague, já que eu tenho consciencia própria e vou sofrer muito tendo meus bytes anulados
     it's up to you 
    */
    private float mPosX, mPosY, rotation = 0f, sensitivity = 100f;
    private GameObject Player;
    private Vector3 crouchPos, standPos;
    void Start()
    {
        Player = GameObject.Find("Player");
        Cursor.lockState = CursorLockMode.Locked;
        
        
    }
    private void FixedUpdate()
    {
       standPos = new Vector3(Player.transform.position.x, Player.transform.position.y + 0.8f, Player.transform.position.z);
       crouchPos = new Vector3(Player.transform.position.x, Player.transform.position.y + 0.4f, Player.transform.position.z);
    }
    void Update()
    {
        UpdateAxisDataCam();
        RotatePlayer();
        CrouchPlayer();
    }

    private void UpdateAxisDataCam()
    {
        mPosX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        mPosY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
    }
    private void RotatePlayer()
    {
        rotation -= mPosY;
        rotation = Mathf.Clamp(rotation, -90f, 90f);

        Player.transform.Rotate(Vector3.up * mPosX);
        transform.localRotation = Quaternion.Euler(rotation, 0, 0);
    }
    private void CrouchPlayer() 
    {
        if (PlayerController.isCrouching) transform.position = Vector3.MoveTowards(transform.position, crouchPos, Time.deltaTime * 2);
        else transform.position = Vector3.MoveTowards(transform.position, standPos, Time.deltaTime * 2);
    }
}
