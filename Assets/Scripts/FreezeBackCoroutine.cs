using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FreezeBackCoroutine : MonoBehaviour
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

    float angleSphere;

    // Start is called before the first frame update
    void Start()
    {
        startCoroutine = false;
    }

    // Update is called once per frame
    void Update()
    {
        // so if the players back is at the wall, the coroutine does not start 
        float angleBorder = Vector3.Angle(cameraGameObject.transform.forward, new Vector3(0, 0, 0) - cameraGameObject.transform.position);
        angleSphere = Vector3.Angle(cameraGameObject.transform.forward, spherePosition - cameraGameObject.transform.position); // todo: does not work when getting closer

        // to debug
        gameText.text = "pos: " + Mathf.Abs(cameraGameObject.transform.position.x).ToString() + "; " + Mathf.Abs(cameraGameObject.transform.position.z).ToString();

        if ((Mathf.Abs(cameraGameObject.transform.position.x) >= RLdistanceX * 0.9 || Mathf.Abs(cameraGameObject.transform.position.z) >= RLdistanceZ * 0.9)
            && (angleBorder >= 90 && angleBorder <= 120) && !startCoroutine)
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
}
