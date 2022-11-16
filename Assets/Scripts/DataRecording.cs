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

    // Start is called before the first frame update
    void Start()
    {
        filename = Application.dataPath + "/test.csv";

        // false = overwrite
        tw = new StreamWriter(filename, false);
        tw.WriteLine("Frame ID; Time; Collision Count; Red Mushroom Count; Brown Mushroom Count; RL Position X; RL Position Y; RL Position Z; Game Position X; Game Position Y; Game Position Z; Navigation State");
        tw.Close();

        countMushroom = rightHandGameObject.GetComponent<CountMushroom>();
        countCollision = cameraGameObject.GetComponent<CountCollision>();
    }

    // Update is called once per frame
    void Update()
    {
         writeCSV();     
    }

    public void writeCSV()
    {
        
        tw = new StreamWriter(filename, true);

        currentTime = currentTime + Time.deltaTime;
        TimeSpan t = TimeSpan.FromSeconds(currentTime);

        tw.WriteLine(
                   Time.frameCount + ";" + t.ToString(@"mm\:ss\:ff") + ";"
                + countCollision.collisionCount + ";" + countMushroom.redMushroomCount + ";" + countMushroom.brownMushroomCount + ";"
                + cameraGameObject.transform.position.x + ";" + cameraGameObject.transform.position.y + ";" + cameraGameObject.transform.position.z + ";"
                + xrOriginGameObject.transform.position.x + ";" + xrOriginGameObject.transform.position.y + ";" + xrOriginGameObject.transform.position.z);

        tw.Close();
        
    }
}
