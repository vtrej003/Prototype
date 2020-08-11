using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;
  
    private Transform _selection;

    // Update is called once per frame
    void Update()
    {
        if (_selection != null)
        {
            Renderer selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            _selection = null;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            Transform selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                Renderer selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    defaultMaterial = selectionRenderer.material;
                    selectionRenderer.material = highlightMaterial;
                }
            }
        }
    }
}
