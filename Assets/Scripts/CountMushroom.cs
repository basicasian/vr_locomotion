using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

// IMPORTANT: check if it has to be attached to the hand
[RequireComponent(typeof(XRDirectInteractor))]
public class CountMushroom : MonoBehaviour
{
    public int redMushroomCount = 0;
    public int brownMushroomCount = 0;

    public TextMeshProUGUI mushroomCountText;

    public GameObject procedureGameObject;
    private Procedure procedure;
    private GenerateScene generateScene;

    private XRDirectInteractor interactor;

    List<IXRInteractable> grabInteractables = new List<IXRInteractable>();

    private void Awake()
    {
        interactor = GetComponent<XRDirectInteractor>();
    }

    // Start is called before the first frame update
    void Start()
    {
        procedure = procedureGameObject.GetComponent<Procedure>();
        generateScene = procedureGameObject.GetComponent<GenerateScene>();

       // mushroomCountText.text = "Red Mushrooms: " + redMushroomCount.ToString() + " / " + generateScene.redMushroom + " \n"
       //   + "Brown Mushrooms: " + brownMushroomCount.ToString() + " / " + generateScene.brownMushroom;
    }

    public void PickUp()
    {
        if (procedure.getPlayingGame())
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
         //   mushroomCountText.text = "Red Mushrooms: " + redMushroomCount.ToString() + " / " + generateScene.redMushroom + " \n"
          //      + "Brown Mushrooms: " + brownMushroomCount.ToString() + " / " + generateScene.brownMushroom;

        }
    }

    public void resetCount()
    {
        redMushroomCount = 0;
        brownMushroomCount = 0;

       // mushroomCountText.text = "Red Mushrooms: " + redMushroomCount.ToString() + " / " + generateScene.redMushroom + " \n"
      //          + "Brown Mushrooms: " + brownMushroomCount.ToString() + " / " + generateScene.brownMushroom;
    }


}
