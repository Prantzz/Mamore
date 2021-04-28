using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurupiraZoneComponent : MonoBehaviour
{
    public bool shouldTriggerOnFirstEnter;
    private bool curupiraActive;
    private bool alreadyActive;
    private GameObject curupira;
    private GameObject player;
    private List<GameObject> activeZone;
    void Start()
    {
        //StartCoroutine(CallCurupira());
        activeZone = new List<GameObject>();
        curupira = GameGlobeData.Curupira;
        foreach(Transform t in transform)
        {
            if(t.TryGetComponent(out MeshRenderer MR))
            {
                MR.enabled = false;
                activeZone.Add(t.gameObject);
            }           
        }
    }
    private void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            if (shouldTriggerOnFirstEnter)
            {
                curupiraActive = true;
                StartCoroutine(CallCurupira());
                shouldTriggerOnFirstEnter = false;
            }
            else
            {
                curupiraActive = true;
                StartCoroutine(CallCurupira());
                shouldTriggerOnFirstEnter = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            alreadyActive = false;
        }
    }
    private IEnumerator EndCurupira()
    {
        Debug.Log("Chamando End Curupira as: " + Time.time);
        
        yield return null;
    }
    private IEnumerator CallCurupira()
    {
        //Debug.Log("Chamando Call Curupira as: " + Time.time);
        //Caso não esteja ativo
        if (!alreadyActive)
        {
            curupira.SetActive(true);
            Vector3 pos = PickRandomPos().position;
            curupira.transform.position = new Vector3(pos.x, 4f, pos.z);
            StartCoroutine(curupira.GetComponent<CurupiraComponent>().ExpandLight());
            GameGlobeData.AU.PullSound(curupira.transform.position, 7, 0,true);
            alreadyActive = true;
        }
        while (alreadyActive)
        {
            ChangeCurupiraPlace();
            yield return new WaitForSeconds(Random.Range(4f,8f));                      
        }
        Debug.Log("Terminando Call Curupira as: " + Time.time);
    }
    private void ChangeCurupiraPlace()
    {
        Vector3 pos = PickRandomPos().position;
        curupira.GetComponent<CurupiraComponent>().destination = new Vector3(pos.x, 4f, pos.z);
    }
    private Transform PickRandomPos()
    {
        return activeZone[Random.Range(0, activeZone.Count-1)].transform;
    }
}
