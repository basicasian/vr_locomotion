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

    public GameObject xrOriginGameObject;
    private Procedure procedure;

    private XRDirectInteractor interactor;
    private GenerateMushroom generateMushroom;


    List<IXRInteractable> grabInteractables = new List<IXRInteractable>();

    private void Awake()
    {
        interactor = GetComponent<XRDirectInteractor>();
    }

    // Start is called before the first frame update
    void Start()
    {
        procedure = xrOriginGameObject.GetComponent<Procedure>();
        generateMushroom = xrOriginGameObject.GetComponent<GenerateMushroom>();

        mushroomCountText.text = "Red Mushrooms: " + redMushroomCount.ToString() + " / " + generateMushroom.redMushrooms + " \n"
          + "Brown Mushrooms: " + brownMushroomCount.ToString() + " / " + generateMushroom.brownMushrooms;


    }

    public void PickUp()
    {
        if (procedure.playingGame)
        {
            interactor.GetValidTargets(grabInteractables);

            foreach (var interactable in grabInteractables)
            {

                if (interactable.transform.CompareTag("RedMushroom"))
                {
                    redMushroomCount++;
                    interactable.transform.gameObject.SetActive(false);
                }

                if (interactable.transform.CompareTag("BrownMushroom"))
                {
                    brownMushroomCount++;
                    interactable.transform.gameObject.SetActive(false);
                }
            }
            mushroomCountText.text = "Red Mushrooms: " + redMushroomCount.ToString() + " / " + generateMushroom.redMushrooms + " \n"
                + "Brown Mushrooms: " + brownMushroomCount.ToString() + " / " + generateMushroom.brownMushrooms;

        }
    }

}
