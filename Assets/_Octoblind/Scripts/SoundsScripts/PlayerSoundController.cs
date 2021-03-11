using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    private PlayerController PlayerController;
    private AudioManager AM;
    private bool isCrouchingBuffer;
    public List<AudioClip> PlayerSounds;
    public AudioClip Walking;
    public AudioSource WalkingAudio;
    public AudioSource Secondary;
    
    void Start()
    {
        isCrouchingBuffer = false;
        AM = GameObject.Find("GameGlobeData").GetComponent<AudioManager>();
        PlayerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        if(PlayerController.CharCon.isGrounded && !WalkingAudio.isPlaying)
        {
            //Som de cair
            AM.PullSound(this.transform.position, 0, 1);
            WalkingAudio.Play();
        }
        if(!isCrouchingBuffer && PlayerController.isCrouching)
        {
            //Som de crouch
            AM.PullSound(this.transform.position, 0, 2);
            isCrouchingBuffer = true;
        }
        if(!PlayerController.isCrouching && isCrouchingBuffer == true)
        {
            //Som de levantar do crouch
            AM.PullSound(this.transform.position, 0, 2);
            isCrouchingBuffer = false;
        }
        //Controla volume do andar
        WalkingAudio.volume = PlayerController.xzMove.magnitude;

        //Controla pitch do andar
        WalkingAudio.pitch = PlayerController.isSprinting ? 1.5f : 1f;
    }
    public void JumpSound()
    {
        //Som do pulo
        AM.PullSound(this.transform.position, 0, 0);
        Invoke("JumpSound_",0.1f); 
    }
    public void JumpSound_()
    {
        WalkingAudio.Stop();
    }
}
