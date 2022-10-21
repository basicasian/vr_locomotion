using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Procedure : MonoBehaviour
{

    float countdownTime = 180.0f;
    Boolean startGame = false;

    public TextMeshProUGUI gameText;
    public TextMeshProUGUI timerText;
    public InputActionReference startGameReference = null;

    public GameObject rightGameObject;
    public GameObject leftGameObject;

    private XRRayInteractor rayInteractor;
    private XRDirectInteractor directInteractor;

    // Start is called before the first frame update
    void Start()
    {
        TimeSpan t = TimeSpan.FromSeconds(countdownTime);
        timerText.text = t.ToString(@"mm\:ss");
        gameText.text = "to start game press the trigger";
    }

    // Update is called once per frame
    void Update()
    {
        if (startGame)
        {

            if (countdownTime > 0)
            {
                countdownTime -= Time.deltaTime;
            }

            TimeSpan t = TimeSpan.FromSeconds(countdownTime);
            timerText.text = t.ToString(@"mm\:ss");

            if (countdownTime < 0)
            {
                gameText.text = "time's up!";

                rightGameObject.SetActive(false);
                leftGameObject.SetActive(false);

                directInteractor.allowActivate = false;
                directInteractor.allowSelect = false;
                rayInteractor.enabled = false;
            }
        }
    }

    private void Awake()
    {
        startGameReference.action.started += StartGame;

        rightGameObject = GameObject.FindGameObjectWithTag("RightDirectHand");
        leftGameObject = GameObject.FindGameObjectWithTag("LeftRayHand");

        directInteractor = rightGameObject.GetComponent<XRDirectInteractor>();
        rayInteractor = leftGameObject.GetComponent<XRRayInteractor>();

    }
    private void StartGame(InputAction.CallbackContext context)
    {
        startGame = true;
        gameText.text = "";
    }

}
