using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TileMovement : MonoBehaviour
{
    /*
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
    */
    //pointer var
    [SerializeField] private bool hasPosition;
    [SerializeField] private GameObject pointer;
    
    //steamvr action input
    public SteamVR_Action_Boolean telePortAction;
    public SteamVR_Behaviour_Pose pose;
    [SerializeField] private bool isTeleporting;

    [SerializeField] private float fadeTime = 0.3f;
    [SerializeField] public float mvSpeed = 1.0f;
    [SerializeField] private float step;

    // Start is called before the first frame update
    void Awake()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    // Update is called once per frame
    void Update()
    {
        hasPosition = UpdatePointer();
        pointer.SetActive(hasPosition);

        if (telePortAction.GetStateUp(pose.inputSource))
        {
            print("get state up is true");
            TryTeleport();
        }
        
    }

    private void TryTeleport()
    {
        //check valid position and if already teleporting
        if (!hasPosition || isTeleporting)
        {
            print("!hasPosition || isTeleporting");
            return;
        }
        
        //Get camera and head position
        Transform cameraRig = SteamVR_Render.Top().transform.root;
        Vector3 headPosition = SteamVR_Render.Top().head.position;
        
        //Figure translation
        Vector3 groundPosition = new Vector3(headPosition.x, cameraRig.position.y, headPosition.z);
        Vector3 translateVector = pointer.transform.position - groundPosition;
        
        //move
        StartCoroutine(MoveRig(cameraRig, translateVector));

    }
    
    private IEnumerator MoveRig(Transform cameraRig, Vector3 translation)
    {
        print("start coroutine");
        //Flag
        isTeleporting = true;
        //Fade to black
        SteamVR_Fade.Start(Color.black, fadeTime, true );
            
        //apply translation
        yield return new WaitForSeconds(fadeTime);
        
        //*trying to lerp A to B ==============
        /*step = mvSpeed * Time.deltaTime;

        Vector3 nextPos = Vector3.MoveTowards(transform.position, translation, step);
        nextPos.y = Terrain.activeTerrain.SampleHeight(nextPos);
        
        cameraRig.position += nextPos;*/
        //END OF LERP===========================
        cameraRig.position += translation;
        //fade to clear
        SteamVR_Fade.Start(Color.clear, fadeTime, true);
        //Deflag
        isTeleporting = false;
    }
    private bool UpdatePointer()
    {
        RaycastHit hit;
        Vector3 dir = transform.TransformDirection(Vector3.forward);
        
        if (Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, dir * hit.distance, Color.yellow);
            pointer.transform.position = hit.collider.bounds.center;
            print("returning true for update pointer");
            return true;
        }
        else
        {
            print("nothit");
            return false;
        }
    }
}
