using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class FreezeBack : MonoBehaviour
{
    public GameObject cameraGameObject; // real life
    public GameObject xrOriginGameObject; // game

    public GameObject sceneGameObject; 

    public int RLdistanceX;
    public int RLdistanceZ;

    public TextMeshProUGUI gameText;

    bool halfTurnDone = false;
    bool sphereCreated = false;
    Vector3 spherePosition;
    GameObject sphere;

    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Mathf.Abs(cameraGameObject.transform.position.x)  + ": " + RLdistanceX * 1.1 ); 
        if (Mathf.Abs(cameraGameObject.transform.position.x) >= RLdistanceX * 0.8 || Mathf.Abs(cameraGameObject.transform.position.z) >= RLdistanceZ * 0.8) 
        {
            

               if (!halfTurnDone)
            {
                StartCoroutine();
            }
               

        } else
        {
            gameText.text = "";
        }

    }

    void StartCoroutine()
    {

        // step 1 
        // deactivate the scene Gameobject, so that the user will not see that they will be tricked and we will rotate the scene after they did their half turn
        sceneGameObject.SetActive(false);

        // step 2
        // add a sphere behind the user 
        if (!sphereCreated)
        {
            sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            spherePosition = cameraGameObject.transform.position - (cameraGameObject.transform.forward * 3); 
            sphere.transform.position = spherePosition;

            sphereCreated = true;
        }

        // step 3 
        // display in canvas 
        gameText.text = "please make a half turn to gaze at the sphere";

        // step 4 
        // check if the rotation of the user is approximately equals to 180 degrees 
        // check if the user face the sphere (angle between camera forward and sphere close to 0) 
        float angle = Vector3.Angle(cameraGameObject.transform.forward, spherePosition - cameraGameObject.transform.position); // todo: does not work when getting closer
        Debug.Log(angle);

        if (angle <= 20)
        {
           halfTurnDone = true;
           gameText.text = "looked at the sphere";

           // step 5
           GameObject.Destroy(sphere);

            // step 6
            // cancel the "virtual half turn"
             xrOriginGameObject.transform.Rotate(new Vector3(0, 180, 0));
             sceneGameObject.SetActive(true);
        }





    }
}
