using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NavigationState : MonoBehaviour
{
    public InputActionReference steeringReference = null;
    public InputActionReference teleportationReference = null;

    public GameObject xrOriginGameObject; // body based steering 
    public GameObject rightHandGameObject; // teleportation 
    public GameObject cameraGameObject; // walking

    private Vector3 previousLocation = Vector3.zero;
    private Vector3 differenceLocation = Vector3.zero;
    public float timeThreshold = 1f;
    public float distanceThreshold = 0.3f;
    private float countdownTime;

    private string currentState = "nothing";


    // Start is called before the first frame update
    void Start()
    {
        countdownTime = timeThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        if (countdownTime > 0)
        {
            countdownTime -= Time.deltaTime;
        } else // everytime when the threshold reached
        {
            countdownTime = timeThreshold;
           
            differenceLocation = previousLocation - cameraGameObject.transform.position;

            Debug.Log("previous: " + previousLocation);
            Debug.Log("current: " + cameraGameObject.transform.position);
            Debug.Log("difference: " + differenceLocation);
            Debug.Log(Mathf.Abs(differenceLocation.x) + ", " + (Mathf.Abs(differenceLocation.y) + ", " + Mathf.Abs(differenceLocation.z)));

            previousLocation = cameraGameObject.transform.position;
        }

        if (steeringReference.action.IsPressed() && xrOriginGameObject.activeSelf)
        {
            currentState = "steering";
        }
        else if (teleportationReference.action.IsPressed() && rightHandGameObject.activeSelf)
        {
            currentState = "teleportation";
        }
        else if ((Mathf.Abs(differenceLocation.x) > distanceThreshold || Mathf.Abs(differenceLocation.z) > distanceThreshold))
        {
            currentState = "walking";
        }
        else
        {
            currentState = "nothing";
        }
       
    }

    public string getNavigationState()
    {
        return currentState;
    }
}
