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
    bool startCoroutine = false;
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
        // so if the players back is at the wall, the coroutine does not start 
        float angleBorder = Vector3.Angle(cameraGameObject.transform.forward, new Vector3(0,0,0) - cameraGameObject.transform.position);
        // Debug.Log(angleBorder);

        if ((Mathf.Abs(cameraGameObject.transform.position.x) >= RLdistanceX * 0.7 || Mathf.Abs(cameraGameObject.transform.position.z) >= RLdistanceZ * 0.7)
            && (angleBorder >= 90 && angleBorder <= 120))
        {
           halfTurnDone = false;
           startCoroutine = true;

        } else
        {
            gameText.text = "";
        }

        if (startCoroutine)
        {
            Coroutine();
        }

    }

    void Coroutine()
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
        float angleSphere = Vector3.Angle(cameraGameObject.transform.forward, spherePosition - cameraGameObject.transform.position); // todo: does not work when getting closer
        if (angleSphere <= 20)
        {
            halfTurnDone = true;
            gameText.text = "looked at the sphere";

            // step 5
            GameObject.Destroy(sphere);
            startCoroutine = false;
            sphereCreated = false;

            // step 6
            // cancel the "virtual half turn"
            xrOriginGameObject.transform.Rotate(new Vector3(0, 180, 0));
           sceneGameObject.SetActive(true);
        }

    }
}
