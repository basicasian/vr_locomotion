using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;
using TMPro;

// has to be attached to main camera because of the capsule collider 
public class CountCollision : MonoBehaviour
{

    public int collisionCount = 0;
    public TextMeshProUGUI countText;

    // Start is called before the first frame update
    void Start()
    {
        countText.text = "Collisions: " + collisionCount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "RedMushroom" && other.tag != "BrownMushroom")
        {
            collisionCount++;
            countText.text = "Collisions: " + collisionCount.ToString();
        }
       
    }


}
