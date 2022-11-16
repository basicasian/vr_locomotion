using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRDirectInteractor))]
public class CountMushroom : MonoBehaviour
{
    public int redMushroomCount = 0;
    public int brownMushroomCount = 0;

    public TextMeshProUGUI mushroomCountText;

    // [SerializeField] private InputActionReference inputActionReference = null;
    private XRDirectInteractor interactor;

    List<IXRInteractable> grabInteractables = new List<IXRInteractable>();

    private void Awake()
    {
        interactor = GetComponent<XRDirectInteractor>();
    }

    // Start is called before the first frame update
    void Start()
    {
        mushroomCountText.text = "Red Mushrooms: " + redMushroomCount.ToString() + "/8 \n"
            + "Brown Mushrooms: " + brownMushroomCount.ToString() + "/10";
    }

    public void PickUp()
    {
        interactor.GetValidTargets(grabInteractables);

        foreach (var interactable in grabInteractables)
        {

            if (interactable.transform.CompareTag("RedMushroom")) {
                redMushroomCount++;
                interactable.transform.gameObject.SetActive(false); 
            }

            if (interactable.transform.CompareTag("BrownMushroom"))
            {
                brownMushroomCount++;
                interactable.transform.gameObject.SetActive(false);
            }
        }
        mushroomCountText.text = "Red Mushrooms: " + redMushroomCount.ToString() + "/8 \n"
            + "Brown Mushrooms: " + brownMushroomCount.ToString() + "/10";

    }

}
