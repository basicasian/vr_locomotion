using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistToMushroom : MonoBehaviour
{

    public GameObject redMushrooms;
    public GameObject brownMushrooms;

    public GameObject prig;
    public GameObject pcamera;

    public double closestMushroom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void FindClosestChild(Procedure.Condition cond)
    {
        GameObject player;
        if (cond.Equals(Procedure.Condition.W))
        {
            player = pcamera;
        }
        else
        {
            player = prig;
        }
        if (redMushrooms.transform.childCount == 0)
        {
            return; // No children to search
        }

        Transform closestChild = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform child in redMushrooms.transform)
        {
            //if (!child.gameObject.activeInHierarchy) continue;
            float distance = Vector3.Distance(player.transform.position, child.position);
            if (distance < closestDistance)
            {
                closestMushroom = distance;
                closestChild = child;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //FindClosestChild(Procedure.Condition.A);
        //Debug.Log(closestMushroom);
    }
}
