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
    private Vector3 RLPosition;
    private Vector3 gamePosition;
    
    private TextWriter tw;
    private float currentTime = 0;

    public CountCollision countCollision;


    // Start is called before the first frame update
    void Start()
    {
        filename = Application.dataPath + "/test.csv";

        // false = overwrite
        tw = new StreamWriter(filename, false);
        tw.WriteLine("Frame ID; Time; Collisions Count; Red Mushroom Count; Brown Mushroom Count; RL Position X; RL Position Y; RL Position Z; Game Position X; Game Position Y; Game Position Z");
        tw.Close();
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
                   Time.frameCount + ";" + t.ToString(@"mm\:ss\:fff") + ";"
                + "countCollision.collisionCount" + ";" + redMushroomCount + ";" + brownMushroomCount + ";"
                + RLPosition.x + ";" + RLPosition.y + ";" + RLPosition.z + ";"
                + gamePosition.x + ";" + gamePosition.y + ";" + gamePosition.z);

        tw.Close();
        
    }
}
