using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Procedure : MonoBehaviour
{

    public float timelimit;
    private float countdown;

    // TODO: change it to private
    public Boolean playingGame = false;
    public Boolean restartedGame = false;

    Boolean startPosition = false;

    public TextMeshProUGUI gameText;
    public TextMeshProUGUI timerText;
    public InputActionReference startGameReference = null;

    public GameObject rightHandGameObject;
    public GameObject leftHandGameObject;
    public GameObject cameraGameObject;
    public GameObject mushroomStumps;

    public BodyBasedSteering bodybasedScript;
    public GenerateMushroom generateMushroom;
    public CountCollision countCollision;
    public CountMushroom countMushroom;

    // Start is called before the first frame update
    void Start()
    {
        TimeSpan t = TimeSpan.FromSeconds(timelimit);
        timerText.text = t.ToString(@"mm\:ss");
        gameText.text = "please go to the yellow mark of the play area";

        countdown = timelimit;

        countMushroom = rightHandGameObject.GetComponent<CountMushroom>();
        countCollision = cameraGameObject.GetComponent<CountCollision>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playingGame && countdown >= 0)
        {
            mushroomStumps.SetActive(false);

            if (cameraGameObject.transform.position.x < 0.5f && cameraGameObject.transform.position.x > -0.5f &&
                cameraGameObject.transform.position.z < 0.5f && cameraGameObject.transform.position.z > -0.5f)
            {
                startPosition = true;
                gameText.text = "to start game press the trackpad";
            }
            else
            {
                startPosition = false;
                gameText.text = "please go to the yellow mark of the play area";
            }
        } 
        else
        {         
            mushroomStumps.SetActive(true);

            if (countdown > 0)
            {
                countdown -= Time.deltaTime;
            }

            TimeSpan t = TimeSpan.FromSeconds(countdown);
            timerText.text = t.ToString(@"mm\:ss");

            if (countdown < 0)
            {
                gameText.text = "time's up!";
                playingGame = false;

                rightHandGameObject.SetActive(false);
                leftHandGameObject.SetActive(false);

                bodybasedScript.SetActive(false);

                // restart game on space
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("Restart!");
                    RestartGame();
                }
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
            playingGame = true;
            gameText.text = "";

        } 
    }

    private void RestartGame()
    {
        countdown = timelimit;
        countCollision.collisionCount = 0;
        countMushroom.redMushroomCount = 0;
        countMushroom.brownMushroomCount = 0;

        generateMushroom.regenerateMushrooms();

    }

}
