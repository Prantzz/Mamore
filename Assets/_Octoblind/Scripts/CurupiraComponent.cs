using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CurupiraComponent : MonoBehaviour
{
    public Light callerLight;
    public Vector3 destination;
    private AudioSource ownAS;
    void Start()
    {
        this.callerLight = transform.GetComponentInChildren<Light>();
        ownAS = GetComponent<AudioSource>();
    }

    void Update()
    {
        if ((destination - transform.position).magnitude > 0.1f)
        {

            this.transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime);
        }
        else if((destination - transform.position).magnitude <= 0.1f && (destination - transform.position).magnitude > 0f)
        {            
            StartCoroutine(ExpandLight());
            GameGlobeData.AU.PullSound(transform.position, 7, 0, true);
            transform.position = destination;
        }
        ownAS.pitch = Mathf.Clamp((destination - transform.position).magnitude,0,2f);
        ownAS.volume = Mathf.Clamp((destination - transform.position).magnitude, 0, 0.8f);
    }
    public IEnumerator ExpandLight()
    {
        bool animatingLight;
        float timeSinceStart = 0;
        animatingLight = true;
        callerLight.enabled = true;
        callerLight.intensity = 400;
        callerLight.range = 70;
        timeSinceStart = 0;
        while (animatingLight)
        {
            yield return new WaitForEndOfFrame();
            timeSinceStart += Time.deltaTime;
            if (callerLight.intensity > 0) callerLight.intensity = 400 - (Mathf.Pow(timeSinceStart,4));
            //if (callerLight.range > 0) callerLight.range = 0.5f * Time.deltaTime;
            if (callerLight.intensity <= 0 && callerLight.range <= 0) animatingLight = false;
        }
        timeSinceStart = 0;
        yield return null;
    }
}
