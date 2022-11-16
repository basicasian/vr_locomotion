using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using static CountCollision;

public class DataRecording : MonoBehaviour
{
    public string filename = "";

    private int frameID;
    private int collisionCount;
    private int redMushroomCount;
    private int brownMushroomCount;
    private Vector3 RLPosition;
    private Vector3 gamePosition;
    
    private TextWriter tw;
    private float currentTime = 0;


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
                + collisionCount + ";" + redMushroomCount + ";" + brownMushroomCount + ";"
                + RLPosition.x + ";" + RLPosition.y + ";" + RLPosition.z + ";"
                + gamePosition.x + ";" + gamePosition.y + ";" + gamePosition.z);
        /*
        tw.WriteLine(
                Time.frameCount + "," + (playerList.player[i].currentTime - Time.deltaTime) + "," 
                + playerList.player[i].collisionCount + "," + playerList.player[i].redMushroomCount + "," + playerList.player[i].brownMushroomCount + ","
                + playerList.player[i].RLPosition.x + "," + playerList.player[i].RLPosition.y + "," + playerList.player[i].RLPosition.z + ","
                + playerList.player[i].gamePosition.x + "," + playerList.player[i].gamePosition.y + "," + playerList.player[i].gamePosition.z);*/

        tw.Close();
        
    }
}
