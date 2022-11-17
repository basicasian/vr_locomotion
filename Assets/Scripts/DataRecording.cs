using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataRecording : MonoBehaviour
{
    public string filename = "";

    private int redMushroomCount;
    private int brownMushroomCount;
    
    private TextWriter tw;
    private float currentTime = 0;

    public GameObject cameraGameObject; // real life
    public GameObject xrOriginGameObject; // game

    public GameObject rightHandGameObject; // mushroom
    private CountMushroom countMushroom;
    private CountCollision countCollision;

    public NavigationState navigationState; // navigation state
    public Procedure procedure; // procedure

    Boolean createdCSV = false;

    // Start is called before the first frame update
    void Start()
    {
        countMushroom = rightHandGameObject.GetComponent<CountMushroom>();
        countCollision = cameraGameObject.GetComponent<CountCollision>();
        procedure = xrOriginGameObject.GetComponent<Procedure>();
    }

    // Update is called once per frame
    void Update()
    {
        if (procedure.startGame)
        {
            writeCSV();
        }
       
    }

    private void writeCSV()
    {
        if (createdCSV)
        {
            createCSV();
        }
        tw = new StreamWriter(filename, true);

        currentTime = currentTime + Time.deltaTime;
        TimeSpan t = TimeSpan.FromSeconds(currentTime);

        tw.WriteLine(
                   Time.frameCount + ";" + t.ToString(@"mm\:ss\:ff") + ";"
                + countCollision.collisionCount + ";" + countMushroom.redMushroomCount + ";" + countMushroom.brownMushroomCount + ";"
                + cameraGameObject.transform.position.x + ";" + cameraGameObject.transform.position.y + ";" + cameraGameObject.transform.position.z + ";"
                + xrOriginGameObject.transform.position.x + ";" + xrOriginGameObject.transform.position.y + ";" + xrOriginGameObject.transform.position.z + ";"
                + navigationState.getNavigationState());

        tw.Close();
        
    }

    private void createCSV()
    {
        DateTime dt = DateTime.Now;
        string dateString = dt.ToString("yyyy-MM-dd--HH-mm-ss");

        filename = Application.dataPath + "/DataRecording/" + dateString + ".csv";

        // false = overwrite
        tw = new StreamWriter(filename, false);
        tw.WriteLine("Frame ID; Time; Collision Count; Red Mushroom Count; Brown Mushroom Count; RL Position X; RL Position Y; RL Position Z; Game Position X; Game Position Y; Game Position Z; Navigation State");
        tw.Close();

        createdCSV = true;
    }


}
