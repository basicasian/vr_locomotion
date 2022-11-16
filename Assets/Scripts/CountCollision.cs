using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;
using TMPro;

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

    public int getCollisionCount()
    {
        return collisionCount;
    }


}
