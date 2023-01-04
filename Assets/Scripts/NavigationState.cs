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
    public float timeThreshold;
    public float distanceThreshold;
    private float countdownTime;

    private NavigationStateEnum currentState = NavigationStateEnum.N;


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
            previousLocation = cameraGameObject.transform.position;

            /*
            Debug.Log("previous: " + previousLocation);
            Debug.Log("current: " + cameraGameObject.transform.position);
            Debug.Log("difference: " + differenceLocation);
            Debug.Log(Mathf.Abs(differenceLocation.x) + ", " + (Mathf.Abs(differenceLocation.y) + ", " + Mathf.Abs(differenceLocation.z)));
            */
        }

        if (((Mathf.Abs(differenceLocation.x) > distanceThreshold || Mathf.Abs(differenceLocation.z) > distanceThreshold)) &&
              steeringReference.action.IsPressed() && xrOriginGameObject.activeSelf && (teleportationReference.action.IsPressed() && rightHandGameObject.activeSelf))
        {
            currentState = NavigationStateEnum.WST;
            return;
        }

        if ((Mathf.Abs(differenceLocation.x) > distanceThreshold || Mathf.Abs(differenceLocation.z) > distanceThreshold) && (steeringReference.action.IsPressed() && xrOriginGameObject.activeSelf))
        {
            currentState = NavigationStateEnum.WS;
            return;
        }
        if (steeringReference.action.IsPressed() && xrOriginGameObject.activeSelf && (teleportationReference.action.IsPressed() && rightHandGameObject.activeSelf))
        {
            currentState = NavigationStateEnum.ST;
            return;
        }

        if ((Mathf.Abs(differenceLocation.x) > distanceThreshold || Mathf.Abs(differenceLocation.z) > distanceThreshold) && (teleportationReference.action.IsPressed() && rightHandGameObject.activeSelf))
        {
            currentState = NavigationStateEnum.WT;
            return;
        }


        if (steeringReference.action.IsPressed() && xrOriginGameObject.activeSelf)
        {
            currentState = NavigationStateEnum.S;
            return;
        }
        // TODO: not sure how to access to teleportation trigger
        if (teleportationReference.action.IsPressed() && rightHandGameObject.activeSelf) 
        {
            currentState = NavigationStateEnum.T;
            return;
        }
        if ((Mathf.Abs(differenceLocation.x) > distanceThreshold || Mathf.Abs(differenceLocation.z) > distanceThreshold))
        {
            Debug.Log("differenceLocation.x: " + Mathf.Abs(differenceLocation.x));
            Debug.Log("differenceLocation.z: " + Mathf.Abs(differenceLocation.z));

            currentState = NavigationStateEnum.W;
            return;
        }

        currentState = NavigationStateEnum.N;
        return;


    }

    public string getNavigationState()
    {
        return currentState.ToString();
    }

}

enum NavigationStateEnum
{ 
    N,
    S,
    T,
    W,
    WS,
    ST,
    WT,
    WST
}
