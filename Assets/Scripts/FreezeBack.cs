using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class FreezeBack : MonoBehaviour
{
    public GameObject cameraGameObject; // real life
    public GameObject xrOriginGameObject; // game

    public int RLdistanceX;
    public int RLdistanceZ;

    public TextMeshProUGUI gameText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Mathf.Abs(cameraGameObject.transform.position.x)  + ": " + RLdistanceX * 1.1 ); 
        if (Mathf.Abs(cameraGameObject.transform.position.x) >= RLdistanceX * 0.8 || Mathf.Abs(cameraGameObject.transform.position.z) >= RLdistanceZ * 0.8) 
        {

            gameText.text = "you are at the border";
            // Debug.Log("you are at the border");

            // in game character should rotate twice as fast
            // so if rl character rotate 180 -> in game character rotate 360 and stays in the right direction
            xrOriginGameObject.transform.Rotate(Vector3.forward, 2);

        } else
        {
            gameText.text = "";
        }

    }
}
