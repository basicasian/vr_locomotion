using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideWorkspace : MonoBehaviour
{
    public static bool playerIsBack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            Debug.Log("Player outside of workspace");
            Procedure[] procedures = FindObjectsOfType(typeof(Procedure)) as Procedure[];
            foreach (Procedure p in procedures)
            {
                if (p.isActiveAndEnabled)
                {
                    playerIsBack = false;
                    p.GetComponent<FreezeBackCoroutine>().Freeze();
                    return;
                }
                else { break; }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            playerIsBack = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
