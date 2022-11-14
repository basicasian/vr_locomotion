using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRDirectInteractor))]
public class CountMushroom : MonoBehaviour
{
    private int countRedMushroom = 0;
    private int countBrownMushroom = 0;

    public TextMeshProUGUI countMushroomText;

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
        countMushroomText.text = "Red Mushrooms: " + countRedMushroom.ToString() + "/8 \n"
            + "Brown Mushrooms: " + countBrownMushroom.ToString() + "/10";
    }

    public void PickUp()
    {
        interactor.GetValidTargets(grabInteractables);

        foreach (var interactable in grabInteractables)
        {

            if (interactable.transform.CompareTag("RedMushroom")) {
                countRedMushroom++;
                interactable.transform.gameObject.SetActive(false); 
            }

            if (interactable.transform.CompareTag("BrownMushroom"))
            {
                countBrownMushroom++;
                interactable.transform.gameObject.SetActive(false);
            }
        }
        countMushroomText.text = "Red Mushrooms: " + countRedMushroom.ToString() + "/8 \n"
            + "Brown Mushrooms: " + countBrownMushroom.ToString() + "/10";


    }

}
