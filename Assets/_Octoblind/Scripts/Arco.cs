using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arco : MonoBehaviour
{
    [SerializeField]
    private GameObject arrow;
    private float shotForce = 0, startingFOV;
    private bool shotIsGauging;

    private void Start()
    {
        startingFOV = Camera.main.fieldOfView;
        shotIsGauging = false;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            shotIsGauging = true;
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            shotIsGauging = false;
            if (shotForce > 0.3f)
            Shot();    
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

        Debug.Log(shotForce);
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
