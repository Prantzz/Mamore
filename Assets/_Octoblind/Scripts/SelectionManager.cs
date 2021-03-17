using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject player;


    [SerializeField] private string selectionTag = "Selectable";
    [SerializeField] private Material highLightMaterial;
    [SerializeField] private Material defaultMaterial;
    private Renderer selectionRenderer;


    private Transform _selection;
    
 
    private void Update()
    {
        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponentInChildren<Renderer>();
            selectionRenderer.material = defaultMaterial;
            _selection.GetComponent<objectController>().isSelected = false;
            _selection = null;

        }
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 3f))
            {
                Transform selection = hit.transform;
                if (selection.tag == selectionTag)
                {               
                    selectionRenderer = selection.GetComponentInChildren<Renderer>();

                if (selectionRenderer != null)
                    {
                        if (selectionRenderer.material != highLightMaterial) selectionRenderer.material = highLightMaterial;
                    }
                    _selection = selection;

                    objectController objCon = selection.GetComponent<objectController>();
                    if (!objCon.isSelected) objCon.isSelected = true;
                }

            }


       

    }

}
