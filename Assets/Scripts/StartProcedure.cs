using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartProcedure : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private int time = 0;

    // Start is called before the first frame update
    void Start()
    {
        timerText.text = "to start game click any button on your controller";
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
