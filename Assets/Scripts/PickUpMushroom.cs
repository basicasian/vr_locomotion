using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class PickUpMushroom : MonoBehaviour
{
    public InputActionReference pickUpReference = null;
    private int countRedMushroom = 0;
    private int countBrownMushroom = 0;
    public TextMeshProUGUI countMushroomText;

    /*
    void Start()
    {
        countMushroomText.text = "Red Mushrooms: " + countRedMushroom.ToString() + "\n"
            + "Brown Mushrooms: " + countBrownMushroom.ToString();
    }*/

    public void PickUp(XRBaseInteractor obj)
    {
        //gameObject.SetActive(false);
        if (gameObject.CompareTag("RedMushroom"))
        {
            countRedMushroom++;
        }

        if (gameObject.CompareTag("BrownMushroom"))
        {
            countBrownMushroom++;
        }

    }
}
