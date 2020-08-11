using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField]  private Transform _selection;
    [SerializeField] public GameObject teleportObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            Transform selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                Collider selectionCollider = selection.GetComponent<Collider>();
                teleportObject.transform.position = selectionCollider.bounds.center;
            }
        }
    }
}
