using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    private string HOLDING_TOOL = "Undefined";
    private Vector3 FirstPlank,SecondPlank;
    private int placedPlanks;

    private void Start()
    {
        FirstPlank = new Vector3(-2.31f, -0.18f, 0f) + gameObject.transform.position;
        SecondPlank = new Vector3(-0.77f, 0f, 0f) + FirstPlank;
    }
    private void Update()
    {
        if(HOLDING_TOOL != Hand.HOLDING_TOOL) HOLDING_TOOL = Hand.HOLDING_TOOL;
        Debug.Log(HOLDING_TOOL);
        placedPlanks = gameObject.transform.childCount;
        if (placedPlanks == 2) GameGlobeData.IsGameOver = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "FixPoint0" || other.tag == "FixPoint1") 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 3f))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (HOLDING_TOOL == "Martelo" && hit.transform.gameObject.tag == "Selectable")
                    {
                        if (gameObject.transform.childCount < 2)
                        {
                            hit.transform.gameObject.transform.parent = gameObject.transform;
                            hit.transform.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                            hit.transform.localRotation = gameObject.transform.rotation;
                            if (hit.transform.gameObject.name == "Dormente0") hit.transform.position = FirstPlank;
                            if (hit.transform.gameObject.name == "Dormente1") hit.transform.position = SecondPlank;

                        }
                    }
                    if (HOLDING_TOOL == "Serra" && hit.transform.gameObject.tag == "Selectable")
                    {
                        Player.INSANITY -= 50;
                    }
                }
                
            }

        }
        }
    }

