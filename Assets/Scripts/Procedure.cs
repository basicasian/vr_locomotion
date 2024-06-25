using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Procedure : MonoBehaviour
{
    public enum Condition
    {
        W,S,T,A
    }
    public enum Workspace
    {
        S,M
    }
    public enum VE
    {
        S,M,L
    }
    public Workspace workspace;
    public VE ve;
    private Condition condition = Condition.A;
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

    public GameObject locomotion;

    public GameObject startGO;

    private BodyBasedSteering bodybasedSteeringScript;
    private GenerateScene generateSceneScript;
    private CountCollision countCollisionScript;
    private CountMushroom countMushroomScript;
    private DataRecording dataRecordingScript;

    private bool generatedBrownMushrooms, canGenerateBrownMushrooms, generatedRedMushrooms, canGenerateRedMushrooms;

    // Start is called before the first frame update
    void Start()
    {
        countdown = timelimit;

        bodybasedSteeringScript = xrOriginGameObject.GetComponent<BodyBasedSteering>();
        countMushroomScript = rightHandGameObject.GetComponent<CountMushroom>();
        countCollisionScript = cameraGameObject.GetComponent<CountCollision>();
        generateSceneScript = GetComponent<GenerateScene>();
        dataRecordingScript = dataRecordingGameObject.GetComponent<DataRecording>();

        generatedBrownMushrooms = true;
        canGenerateBrownMushrooms = false;
        generatedRedMushrooms = true;
        canGenerateRedMushrooms = false;

        if (workspace.Equals(Workspace.S))
        {
            GameObject.Find("BigWS").SetActive(false);
        }
        else
        {
            GameObject.Find("SmallWS").SetActive(false);
        }
    }

    public Condition GetCondition()
    {
        return condition;
    }

    public VE getVE()
    {
        return ve;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: remove this 
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Restart!");
            RestartGame();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartGame();
        }

        // before the game
        if (!playingGame && countdown >= 0 && !gameDone)
        {
            mushroomStumpsGameObject.SetActive(false);

            if (cameraGameObject.transform.position.x < 0.5f && cameraGameObject.transform.position.x > -0.5f &&
                cameraGameObject.transform.position.z < 0.5f && cameraGameObject.transform.position.z > -0.5f)
            {
                startPosition = true;
                gameText.text = "to start game press S key";
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

            GetComponent<DistToMushroom>().FindClosestChild(condition);

            gameText.text = "";
            if (countdown > 0 && !gameDone)
            {
                countdown -= Time.deltaTime;
                TimeSpan t = TimeSpan.FromSeconds(countdown);
                timerText.text = t.ToString(@"mm\:ss");
            }

            // win condition
            if((countMushroomScript.redMushroomCount % generateSceneScript.redMushroom) == 1)
            {
                canGenerateRedMushrooms = true;
            }
            if ((canGenerateRedMushrooms && countMushroomScript.redMushroomCount != 0 && (countMushroomScript.redMushroomCount % generateSceneScript.redMushroom) == 0))
            {
                canGenerateRedMushrooms = false;
                //gameText.text = "good job! \n all mushrooms found!";
                //gameFinished();
                //generate new mushrooms
                Debug.Log("Generate new red mushrooms");
                GetComponent<GenerateScene>().generateMushrooms(0);
            }

            // win condition
            if((countMushroomScript.brownMushroomCount % generateSceneScript.brownMushroom) == 1)
            {
                canGenerateBrownMushrooms = true;
            }
            if (canGenerateBrownMushrooms && countMushroomScript.brownMushroomCount != 0 && (countMushroomScript.brownMushroomCount % generateSceneScript.brownMushroom) == 0)
            {
                canGenerateBrownMushrooms = false;
                //gameText.text = "good job! \n all mushrooms found!";
                //gameFinished();
                //generate new mushrooms
                Debug.Log("Generate new brown mushrooms");
                GetComponent<GenerateScene>().generateMushrooms(1);
            }

            // lose condition 
            if (countdown < 0)
            {
                gameText.text = "time's up! take off the HMD";
                gameFinished();
            }

        }
    }

    public void SetGenerateMushroom(bool b)
    {
        generatedBrownMushrooms = b;
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
            // activate locomotion script only when game starts and based on condition
            if (condition.Equals(Condition.A))
            {
                xrOriginGameObject.GetComponent<BodyBasedSteering>().enabled = true;
                locomotion.GetComponent<TeleportationProvider>().enabled = true;
            }
            if (condition.Equals(Condition.W))
            {
                xrOriginGameObject.GetComponent<BodyBasedSteering>().enabled = false;
                locomotion.GetComponent<TeleportationProvider>().enabled = false;
            }
            if (condition.Equals(Condition.S))
            {
                xrOriginGameObject.GetComponent<BodyBasedSteering>().enabled = true;
                locomotion.GetComponent<TeleportationProvider>().enabled = false;
            }
            if (condition.Equals(Condition.T))
            {
                xrOriginGameObject.GetComponent<BodyBasedSteering>().enabled = false;
                locomotion.GetComponent<TeleportationProvider>().enabled = true;
            }
        }
        startGO.SetActive(false);
    }

    private void StartGame()
    {
        if (startPosition)
        {
            playingGame = true;
            gameText.text = "";
            // activate locomotion script only when game starts and based on condition
            if (condition.Equals(Condition.A))
            {
                xrOriginGameObject.GetComponent<BodyBasedSteering>().enabled = true;
                locomotion.GetComponent<TeleportationProvider>().enabled = true;
            }
            if (condition.Equals(Condition.W))
            {
                xrOriginGameObject.GetComponent<BodyBasedSteering>().enabled = false;
                locomotion.GetComponent<TeleportationProvider>().enabled = false;
            }
            if (condition.Equals(Condition.S))
            {
                xrOriginGameObject.GetComponent<BodyBasedSteering>().enabled = true;
                locomotion.GetComponent<TeleportationProvider>().enabled = false;
            }
            if (condition.Equals(Condition.T))
            {
                xrOriginGameObject.GetComponent<BodyBasedSteering>().enabled = false;
                locomotion.GetComponent<TeleportationProvider>().enabled = true;
            }
        }
        startGO.SetActive(false);
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

        xrOriginGameObject.transform.position = Vector3.zero;
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
