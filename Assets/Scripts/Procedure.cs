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
    private Boolean playingGame = false;
    private Boolean gameDone = false;
    private Boolean startPosition = false;

    public TextMeshProUGUI gameText;
    public TextMeshProUGUI timerText;
    public InputActionReference startGameReference = null;

    public GameObject xrOriginGameObject;
    public GameObject rightHandGameObject;
    public GameObject leftHandGameObject;
    public GameObject cameraGameObject;
    public GameObject mushroomStumpsGameObject;
    public GameObject dataRecordingGameObject;

    private BodyBasedSteering bodybasedSteeringScript;
    private GenerateScene generateSceneScript;
    private CountCollision countCollisionScript;
    private CountMushroom countMushroomScript;
    private DataRecording dataRecordingScript;

    // Start is called before the first frame update
    void Start()
    {
        countdown = timelimit;

        bodybasedSteeringScript = xrOriginGameObject.GetComponent<BodyBasedSteering>();
        countMushroomScript = rightHandGameObject.GetComponent<CountMushroom>();
        countCollisionScript = cameraGameObject.GetComponent<CountCollision>();
        generateSceneScript = GetComponent<GenerateScene>();
        dataRecordingScript = dataRecordingGameObject.GetComponent<DataRecording>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Restart!");
            RestartGame();
        }

        // before the game
        if (!playingGame && countdown >= 0 && !gameDone)
        {
            // mushroomStumpsGameObject.SetActive(false);

            if (cameraGameObject.transform.position.x < 0.5f && cameraGameObject.transform.position.x > -0.5f &&
                cameraGameObject.transform.position.z < 0.5f && cameraGameObject.transform.position.z > -0.5f)
            {
                startPosition = true;
                gameText.text = "to start game press the trackpad";
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    Debug.Log("Generate Scene!");
                    generateSceneScript.generateScene();
                }

                startPosition = false;
                TimeSpan t = TimeSpan.FromSeconds(timelimit);
                timerText.text = t.ToString(@"mm\:ss");

                gameText.text = "please go to the yellow mark of the play area";
            }
        } 
        else
        {
            // during game
            mushroomStumpsGameObject.SetActive(true);

            if (countdown > 0 && !gameDone)
            {
                countdown -= Time.deltaTime;
                TimeSpan t = TimeSpan.FromSeconds(countdown);
                timerText.text = t.ToString(@"mm\:ss");
            }

            // win condition
            if (countMushroomScript.redMushroomCount == generateSceneScript.redMushroom && countMushroomScript.brownMushroomCount == generateSceneScript.brownMushroom)
            {
                gameText.text = "good job! \n all mushrooms found!";
                gameFinished();
               
            }

            // lose condition 
            if (countdown < 0)
            {
                gameText.text = "time's up!";
                gameFinished();
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
    private void gameFinished()
    {
        playingGame = false;
        gameDone = true;

        rightHandGameObject.SetActive(false);
        leftHandGameObject.SetActive(false);
        bodybasedSteeringScript.SetActive(false);

        // restart game on space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Restart!");
            RestartGame();
        }
    }

    private void RestartGame()
    {
        gameDone = false;

        countdown = timelimit;
        countCollisionScript.collisionCount = 0;
        countMushroomScript.redMushroomCount = 0;
        countMushroomScript.brownMushroomCount = 0;

        rightHandGameObject.SetActive(true);
        leftHandGameObject.SetActive(true);
        bodybasedSteeringScript.SetActive(true);

        countCollisionScript.resetCount();
        countMushroomScript.resetCount();
        dataRecordingScript.setCreatedCSV(false);

        generateSceneScript.destroyScene();
        generateSceneScript.generateScene();
    }

    public Boolean getPlayingGame()
    {
        return playingGame;
    }

    public Boolean getGameDone()
    {
        return gameDone;
    }

}
