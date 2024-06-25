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
    public DistanceTracker distVE;
    public DistanceTracker distRE;

    Boolean createdCSV = false;

    private int frameID;
    private StreamWriter tw;

    // Start is called before the first frame update
    void Start()
    {
        countMushroom = rightHandGameObject.GetComponent<CountMushroom>();
        countCollision = cameraGameObject.GetComponent<CountCollision>();
        procedure = procedureGameObject.GetComponent<Procedure>();
        frameID = 0;
        createCSV();
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

        currentTime = currentTime + Time.deltaTime;
        TimeSpan t = TimeSpan.FromSeconds(currentTime);

        tw.WriteLine(
                   frameID++ + ";" + currentTime + ";"
                + countCollision.collisionCount + ";" + countMushroom.redMushroomCount + ";" + countMushroom.brownMushroomCount + ";"
                + cameraGameObject.transform.localPosition.x + ";" + cameraGameObject.transform.localPosition.y + ";" + cameraGameObject.transform.localPosition.z + ";"
                + xrOriginGameObject.transform.position.x + ";" + xrOriginGameObject.transform.position.y + ";" + xrOriginGameObject.transform.position.z + ";"
                + cameraGameObject.transform.rotation.x + ";" + cameraGameObject.transform.rotation.y + ";" + cameraGameObject.transform.rotation.z + ";"
                + navigationState.getNavigationState() + ";" + distMushroom.closestMushroom + ";" + distVE.totalDistance + ";" + distRE.totalDistance );
        
    }

    private void OnDestroy()
    {
        tw.Close();
    }

    private void createCSV()
    {
        DateTime dt = DateTime.Now;
        //string dateString = dt.ToString("yyyy-MM-dd--HH-mm-ss");
        filename = "DataRecording/" + procedure.GetCondition().ToString() + "-" + procedure.workspace.ToString() + "-" + procedure.ve.ToString() + ".csv";

        // false = overwrite
        tw = new StreamWriter(filename, false);
        tw.WriteLine("FrameID;Time;CollisionCount;RedMushroomCount;BrownMushroomCount;" +
            "RLPositionX;RLPositionY;RLPositionZ;GamePositionX;GamePositionY;GamePositionZ;" +
            "RotationX;RotationY;RotationZ;" +
            "NavigationState;" + "DistClosestMushroom;DistanceVE;DistanceRE");

        createdCSV = true;
    }

    public void setCreatedCSV(Boolean value)
    {
        createdCSV = value;
    }


}
