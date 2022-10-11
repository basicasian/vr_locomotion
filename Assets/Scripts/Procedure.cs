using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;

public class Procedure : MonoBehaviour
{

    float countdownTime = 180.0f;
    Boolean startGame = false;

    public TextMeshProUGUI timerText;
    public InputActionReference startGameReference = null;

    // Start is called before the first frame update
    void Start()
    {
        timerText.text = "to start game click any button on your controller";
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
                timerText.text = "time's up!";
            }
        }
    }

    private void Awake()
    {
        startGameReference.action.started += StartGame;
    }
    private void StartGame(InputAction.CallbackContext context)
    {
        startGame = true;
    }

}
