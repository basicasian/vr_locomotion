using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Procedure : MonoBehaviour
{

    public float countdownTime;
    Boolean startGame = false;
    Boolean startPosition = false;

    public TextMeshProUGUI gameText;
    public TextMeshProUGUI timerText;
    public InputActionReference startGameReference = null;

    public GameObject rightHandGameObject;
    public GameObject leftHandGameObject;
    public GameObject cameraGameObject;

    public BodyBasedSteering bodybasedScript;
        
    // Start is called before the first frame update
    void Start()
    {
        TimeSpan t = TimeSpan.FromSeconds(countdownTime);
        timerText.text = t.ToString(@"mm\:ss");
        gameText.text = "please go to the yellow mark of the play area";

    }

    // Update is called once per frame
    void Update()
    {
        if (!startGame)
        {
            if (cameraGameObject.transform.position.x < 0.5f && cameraGameObject.transform.position.x > -0.5f &&
                cameraGameObject.transform.position.z < 0.5f && cameraGameObject.transform.position.z > -0.5f)
            {
                startPosition = true;
                gameText.text = "to start game press the trigger";
            }
            else
            {
                startPosition = false;
                gameText.text = "please go to the yellow mark of the play area";
            }
        } 
        else 
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

       // not needed because game objects are public
       // rightHandGameObject = GameObject.FindGameObjectWithTag("RightDirectHand");
       // leftHandGameObject = GameObject.FindGameObjectWithTag("LeftRayHand");
;
    }
    private void StartGame(InputAction.CallbackContext context)
    {
        if (startPosition)
        {
            startGame = true;
            gameText.text = "";

        } 
       
    }

}
