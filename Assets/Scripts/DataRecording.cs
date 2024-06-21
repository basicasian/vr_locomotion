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

    public GameObject procedureGameObject;
    public NavigationState navigationState; // navigation state
    private Procedure procedure; // procedure

    public DistToMushroom distMushroom;

    Boolean createdCSV = false;

    // Start is called before the first frame update
    void Start()
    {
        countMushroom = rightHandGameObject.GetComponent<CountMushroom>();
        countCollision = cameraGameObject.GetComponent<CountCollision>();
        procedure = procedureGameObject.GetComponent<Procedure>();
    }

    // Update is called once per frame
    void Update()
    {
        // only records as long game is played
        if (procedure.getPlayingGame())
        {
            writeCSV();
        }
       
    }

    private void writeCSV()
    {
        if (!createdCSV)
        {
            createCSV();
        }
        tw = new StreamWriter(filename, true);

        currentTime = currentTime + Time.deltaTime;
        TimeSpan t = TimeSpan.FromSeconds(currentTime);

        tw.WriteLine(
                   Time.frameCount + ";" + currentTime + ";"
                + countCollision.collisionCount + ";" + countMushroom.redMushroomCount + ";" + countMushroom.brownMushroomCount + ";"
                + cameraGameObject.transform.localPosition.x + ";" + cameraGameObject.transform.localPosition.y + ";" + cameraGameObject.transform.localPosition.z + ";"
                + xrOriginGameObject.transform.position.x + ";" + xrOriginGameObject.transform.position.y + ";" + xrOriginGameObject.transform.position.z + ";"
                + cameraGameObject.transform.rotation.x + ";" + cameraGameObject.transform.rotation.y + ";" + cameraGameObject.transform.rotation.z + ";"
                + navigationState.getNavigationState() + ";" + distMushroom.closestMushroom);

        tw.Close();
        
    }

    private void createCSV()
    {
        DateTime dt = DateTime.Now;
        //string dateString = dt.ToString("yyyy-MM-dd--HH-mm-ss");

        string ffilename = "DataRecording/" + filename + ".csv";

        // false = overwrite
        tw = new StreamWriter(ffilename, false);
        tw.WriteLine("FrameID;Time;CollisionCount;RedMushroomCount;BrownMushroomCount;" +
            "RLPositionX;RLPositionY;RLPositionZ;GamePositionX;GamePositionY;GamePositionZ;" +
            "RotationX;RotationY;RotationZ;" +
            "NavigationState;" + "DistClosestMushroom");
        tw.Close();

        createdCSV = true;
    }

    public void setCreatedCSV(Boolean value)
    {
        createdCSV = value;
    }


}
