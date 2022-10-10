using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;
using TMPro;

public class CountCollision : MonoBehaviour
{
    public XROrigin xrOrigin = null;
    private int count = 0;

    public TextMeshProUGUI countText;

    private void OnTriggerEnter(Collider other)
    {
        count++;
        countText.text = "Collisions: " + count.ToString() ;
        //countText.text +=  " - with" + other.ToString();

    }

}
