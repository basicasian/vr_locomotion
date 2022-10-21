using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class CountMushroom : MonoBehaviour
{
    private int countRedMushroom = 0;
    private int countBrownMushroom = 0;

    public TextMeshProUGUI countMushroomText;


    // Start is called before the first frame update
    void Start()
    {
        countMushroomText.text = "Red Mushrooms: " + countRedMushroom.ToString() + "\n"
            + "Brown Mushrooms: " + countBrownMushroom.ToString();
    }

    public void OnSelectExit(XRBaseInteractable interactable)
    {
        if (interactable.CompareTag("RedMushroom"))
        {
            countRedMushroom++;
        }

        if (interactable.CompareTag("BrownMushroom"))
        {
            countBrownMushroom++;
        }
    }

    public void PickUp()
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
