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


    private Transform _selection,_outline;
    
 
    private void Update()
    {
        if (_selection != null)
        {
            _selection.GetComponent<objectController>().isSelected = false;
            _selection = null;
        }
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1.5f))
        {
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

                objectController objCon = selection.GetComponent<objectController>();
                if (!objCon.isSelected) objCon.isSelected = true;
            }

        }
        else
        {
            if (_outline != null)
            {
                if (_outline.GetComponent<Outlinable>() != null)
                {
                    Destroy(_outline.GetComponent<Outlinable>());
                }
            }

            _outline = null;
        }


       

    }

}
