using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Spell : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private ParticleSystem spell;
    public SteamVR_Action_Boolean spellAction;
    public SteamVR_Behaviour_Pose pose;
    //private LineRenderer spellLine;
    void Awake()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
    }
    
    void Start()
    {
       
    }
    
    
    void CastSpell()
    {
        RaycastHit hit;
        Vector3 dir = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            Debug.DrawRay(transform.position, dir * hit.distance, Color.yellow);
            Transform selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                //spellLine.SetPosition(0,transform.position);
                //spellLine.SetPosition(1,selection.position);
                //spellLine.SetWidth(1.0f,1.0f);
                //spellLine.enabled = true;
                Collider selectionCollider = selection.GetComponent<Collider>();
                spell.transform.position = selectionCollider.bounds.center;
                spell.Play();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 dir = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            Debug.DrawRay(transform.position, dir * hit.distance, Color.yellow);
        }
        
        if (spellAction.GetStateUp(pose.inputSource))
        {
            print("this is right controller cast");
            CastSpell();
        }
    }
}
