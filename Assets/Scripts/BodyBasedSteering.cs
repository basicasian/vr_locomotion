using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;

public class BodyBasedSteering : MonoBehaviour
{
    public InputActionReference steeringReference = null;
    public Camera mainCamera = null;
    public XROrigin xrOrigin = null;

    public float speed = 0;

    private bool isActive = true;

    private void Update()
    {
        if (steeringReference.action.IsPressed() && isActive)
        {
            Steering();
        }
    }

    private void Steering()
    {
        Vector3 deltaSteering = (Vector3.Scale(mainCamera.transform.forward, new Vector3(1.0f, 0.0f, 1.0f))); 
        transform.position += deltaSteering * speed * Time.deltaTime;
    }

    public void SetActive(bool value)
    {
        isActive = value;
    }

    public bool getActive()
    {
        return isActive;
    }

}
