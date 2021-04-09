using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EPOOutline;


public class SelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject player;


    [SerializeField] private string selectionTag = "Selectable";
    [SerializeField] private Material highLightMaterial;
    [SerializeField] private Material defaultMaterial;
    private Renderer selectionRenderer;

    objectController objCon;
    private Ray ray;
    private RaycastHit hit;
    private Transform _selection,_outline;

    private bool OutlineChecking;
    
 
    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        
        if (Physics.Raycast(ray, out hit, 2f))
        {
            
            OutlineChecking = true;
            Transform selection = hit.transform;
            if (selection.tag == selectionTag)
            {
                var selectGO = selection.gameObject;
                if (selectGO.GetComponent<Outlinable>() == null)
                    selectGO.AddComponent<Outlinable>();

                if (selectGO.GetComponent<Outlinable>() != null)
                {
                    var selectGoOutlineable = selectGO.GetComponent<Outlinable>();
                    if (selectGoOutlineable.OutlineTargets.Count == 0)
                    {
                        selectGoOutlineable.OutlineTargets.Add(new OutlineTarget(selectGO.GetComponent<Renderer>()));
                    }
                }
                _selection = selection;
                _outline = selection;

                objCon = selection.GetComponent<objectController>();
                if (!objCon.isSelected) objCon.isSelected = true;
            }
            if (selection.tag != selectionTag)
            {
                if (_selection != null)
                {
                    if (_selection.GetComponent<objectController>() != null)
                    {
                        _selection.GetComponent<objectController>().isSelected = false;
                    }
                }
                if (objCon != null)
                {
                    objCon.isSelected = false;
                    if (objCon.isSelected == false || _selection.GetComponent<objectController>().isSelected == false)
                    {
                        if (_outline != null)
                        {
                            if (_outline.GetComponent<Outlinable>() != null)
                            {
                                Destroy(_outline.GetComponent<Outlinable>());
                            }
                        }
                    }
                }
                _selection = null;
            }


        }
        else if (Physics.Raycast(ray, out hit, 200000f)) 
        {
            Transform selection = hit.transform;
            if (selection.tag != selectionTag)
            {
                if (_selection != null)
                {
                    if (_selection.GetComponent<objectController>() != null)
                    {
                        _selection.GetComponent<objectController>().isSelected = false;
                    }
                }
                if (objCon != null)
                {
                    objCon.isSelected = false;
                    if (objCon.isSelected == false || _selection.GetComponent<objectController>().isSelected == false)
                    {
                        if (_outline != null)
                        {
                            if (_outline.GetComponent<Outlinable>() != null)
                            {
                                Destroy(_outline.GetComponent<Outlinable>());
                            }
                        }
                    }
                }
                _selection = null;
            }
        }
       

    }

}
