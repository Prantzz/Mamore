using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arco : MonoBehaviour
{
    [SerializeField]
    private GameObject arrow;
    private float shotForce = 0, startingFOV;
    private bool shotIsGauging;
    private AudioSource ownAU;

    private void Start()
    {
        startingFOV = Camera.main.fieldOfView;
        shotIsGauging = false;
        ownAU = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            ownAU.Play();
            shotIsGauging = true;
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            ownAU.Stop();
            shotIsGauging = false;
            if (shotForce > 0.3f)
            {
                Shot();
                GameGlobeData.AU.PullSound(this.transform.position, 9, 0);
            }
               
        }

        if (shotIsGauging)
        {
            if(shotForce <= 1) 
            {
                shotForce += 0.7f * Time.deltaTime;
                Camera.main.fieldOfView -= shotForce * 10 * Time.deltaTime;
            }
        }
        else 
        {
            if(shotForce > 0) 
            {
                shotForce = 0f;
                Camera.main.fieldOfView = startingFOV;
            }
        }

      //  Debug.Log(shotForce); // comentei esse debug pq tava travando dmais no meu pc, se eu esquecer de descomentar forgive-me i'm just a human, get out bitch, get out the way
    }

    private void Shot() 
    {
        var arrowInst = Instantiate(arrow, gameObject.transform, false);
        var arrowRB = arrowInst.GetComponent<Rigidbody>();
        arrowInst.transform.localRotation = Quaternion.Euler(115, -90, 0);
        arrowRB.AddForce(Camera.main.transform.forward * shotForce * 1000);
        arrowInst.transform.parent = null;
        
    }
}
