using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Procedure : MonoBehaviour
{

    float countdownTime = 120.0f;
    Boolean startGame = false;

    public TextMeshProUGUI gameText;
    public TextMeshProUGUI timerText;
    public InputActionReference startGameReference = null;

    public GameObject rightHandGameObject;
    public GameObject leftHandGameObject;

    public BodyBasedSteering bodybasedScript;
        
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

                rightHandGameObject.SetActive(false);
                leftHandGameObject.SetActive(false);

                bodybasedScript.SetActive(false);
            }
        }
    }

    private void Awake()
    {
        startGameReference.action.started += StartGame;

        rightHandGameObject = GameObject.FindGameObjectWithTag("RightDirectHand");
        leftHandGameObject = GameObject.FindGameObjectWithTag("LeftRayHand");
;
    }
    private void StartGame(InputAction.CallbackContext context)
    {
        startGame = true;
        gameText.text = "";
    }

}
