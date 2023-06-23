using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class FreezeBackCoroutine : MonoBehaviour
{
    public GameObject cameraGameObject; // real life
    public GameObject xrOriginGameObject; // game

    public GameObject sceneGameObject;

    float RLMinDistanceX;
    float RLMaxDistanceX;
    float RLMinDistanceZ;
    float RLMaxDistanceZ;   

    public TextMeshProUGUI gameText;

    bool halfTurnDone = false;
    bool startCoroutine = false;
    bool sphereCreated = false;
    Vector3 spherePosition;
    GameObject sphere;

    Vector3 startPosition;

    float angleSphere;

    List<Vector3> boundaryPoints;

    public float distTriggerFreezeBackup;


    // Start is called before the first frame update
    void Start()
    {
        startCoroutine = false;
        // get boundaries of SteamVR workspace
        GetBoundaryVertices();
    }

    // Update is called once per frame
    void Update()
    {
        // so if the players back is at the wall, the coroutine does not start 
        float angleBorder = Vector3.Angle(Vector3.ProjectOnPlane(cameraGameObject.transform.forward,Vector3.up), Vector3.ProjectOnPlane(cameraGameObject.transform.position- Vector3.zero, Vector3.up));
        angleSphere = Vector3.Angle(cameraGameObject.transform.forward, spherePosition - cameraGameObject.transform.position); // todo: does not work when getting closer

        // to debug
        gameText.text = "pos: " + Mathf.Abs(cameraGameObject.transform.localPosition.x).ToString() + "; " + Mathf.Abs(cameraGameObject.transform.localPosition.z).ToString() + ";"  + angleBorder.ToString();

        
        if ( ( Mathf.Abs(cameraGameObject.transform.localPosition.x) >= RLMaxDistanceX * distTriggerFreezeBackup || Mathf.Abs(cameraGameObject.transform.localPosition.z) >= RLMaxDistanceZ * distTriggerFreezeBackup) && angleBorder <= 90 && !startCoroutine)
        {
            halfTurnDone = false;
            startCoroutine = true;
            StartCoroutine("FreezeTurn");
        } 
    }

    IEnumerator FreezeTurn()
    {

        // step 1 
        // deactivate the scene Gameobject, so that the user will not see that they will be tricked and we will rotate the scene after they did their half turn
        sceneGameObject.SetActive(false);

        // step 2
        // add a sphere behind the user 
        if (!sphereCreated)
        {
            sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            spherePosition = Vector3.ProjectOnPlane(cameraGameObject.transform.position, Vector3.up) - (Vector3.ProjectOnPlane(cameraGameObject.transform.forward, Vector3.up) * 1.5f);
            spherePosition.y = cameraGameObject.transform.position.y;
            sphere.transform.position = spherePosition;
            sphere.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);

            sphereCreated = true;
        }

        // step 3 
        // display in canvas 
        gameText.text = "please make a half turn to gaze at the sphere";

        // step 4 
        // check if the rotation of the user is approximately equals to 180 degrees 
        // check if the user face the sphere (angle between camera forward and sphere close to 0) 
        yield return new WaitUntil(() => angleSphere <= 20);

        halfTurnDone = true;
        gameText.text = "looked at the sphere";

        // step 5
        if(sphereCreated) GameObject.Destroy(sphere);
        startCoroutine = false;
        sphereCreated = false;

        // step 6
        // cancel the "virtual half turn"
        xrOriginGameObject.transform.Rotate(new Vector3(0, 180, 0));
        sceneGameObject.SetActive(true);
        gameText.text = "";
    }

    public void GetBoundaryVertices()
    {
        Debug.Log("check steam VR boundaries");
        List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();
        SubsystemManager.GetInstances<XRInputSubsystem>(subsystems);

        // make sure I actually have a subsystem loaded
        if (subsystems.Count > 0)
        {
            // create a List of Vec3 that will be filled with the vertices
            boundaryPoints = new List<Vector3>();

            int i = 0;
            // if this returns true, then the subsystems supports a boundary and it should have filled our list with them
            if (subsystems[0].TryGetBoundaryPoints(boundaryPoints))
            {
                foreach (Vector3 pos in boundaryPoints)
                {
                    Debug.Log("point " + i++ + " : " + pos);
                    // put a cube at each corner (for debug purpose)
                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), pos, Quaternion.identity);                    
                }
            }
            // set min max distance (assuming we will always make rectangular or sqaure workspace
            RLMinDistanceX = boundaryPoints[0].x;
            RLMaxDistanceX = boundaryPoints[2].x;
            RLMinDistanceZ = boundaryPoints[0].z;
            RLMaxDistanceZ = boundaryPoints[2].z;
            Debug.Log(RLMaxDistanceX + " " + RLMaxDistanceZ);
        }


    }
}
