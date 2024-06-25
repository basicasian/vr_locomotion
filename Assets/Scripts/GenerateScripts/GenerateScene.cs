using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GenerateScene : MonoBehaviour
{
    // IMPORTANT! do not set too small or else while condition will never end
    public int distanceX;
    public int distanceZ;

    // mushrooms
    public GameObject redMushroomParent;
    public GameObject redMushroomPreFab;
    public int redMushroom = 0;

    public GameObject brownMushroomParent;
    public GameObject brownMushroomPreFab;
    public int brownMushroom = 0;

    // trees 
    public GameObject treeParent;
    public GameObject firPreFab;
    public GameObject oakPreFab;
    public GameObject palmPreFab;
    public GameObject poplarPreFab;
    public GameObject tree1PreFab;
    public GameObject tree2PreFab;
    public GameObject tree3PreFab;
    public GameObject tree4PreFab;
    public GameObject tree5PreFab;
    public int trees = 0;

    // grass 
    public GameObject grassParent;
    public GameObject grassPreFab;
    public int grass = 0;

    // rocks 
    public GameObject rockParent;
    public GameObject rockPrefab;
    public int rock = 0;

    // log 
    public GameObject logParent;
    public GameObject logPrefab;
    public int log = 0;

    // bush 
    public GameObject bushParent;
    public GameObject bushPrefab;
    public int bush = 0;

    private Random rand = new Random();

    // Start is called before the first frame update
    void Start()
    {
        generateScene();
    }

    public void generateMushrooms(int mushroomType)
    {
        if(mushroomType == 0) generatePrefab(redMushroom, redMushroomParent, redMushroomPreFab);
        else generatePrefab(brownMushroom, brownMushroomParent, brownMushroomPreFab);
    }

    public void generateScene()
    {
        // mushrooms
        generatePrefab(redMushroom, redMushroomParent, redMushroomPreFab);
        generatePrefab(brownMushroom, brownMushroomParent, brownMushroomPreFab);

        // trees
        generatePrefab(trees, treeParent, firPreFab);
        generatePrefab(trees, treeParent, oakPreFab);
        generatePrefab(trees, treeParent, palmPreFab);
        generatePrefab(trees, treeParent, poplarPreFab);
        if (GetComponent<Procedure>().getVE().Equals(Procedure.VE.S))
        {
             generatePrefab(trees, treeParent, tree1PreFab);
             generatePrefab(trees, treeParent, tree2PreFab);
             generatePrefab(trees, treeParent, tree3PreFab);
             generatePrefab(trees, treeParent, tree4PreFab);
            //generatePrefab(trees, treeParent, tree5PreFab);
        }


        // log
        generatePrefab(log, logParent, logPrefab);

        // grass
        generatePrefab(grass, grassParent, grassPreFab);

        // rocks
        generatePrefab(rock, rockParent, rockPrefab);

        // bush
        generatePrefab(bush, bushParent, bushPrefab);
       
    }

    
    private float nextFloat(Random rnd, double minValue, double maxValue)
    {
        return (float)( rnd.NextDouble() * (maxValue - minValue) + minValue);
    }

    private void generatePrefab(int numberPrefabs, GameObject parent, GameObject prefab)
    {
        int breakCouter = 0;

        while (numberPrefabs != 0)
        {
            // create random positions

            float randomPosX = nextFloat(rand, -distanceX, distanceX);
            float randomPosZ = nextFloat(rand, -distanceZ, distanceZ);
            Vector3 randomPosition = new Vector3(randomPosX, 0, randomPosZ);

            // create random roations
            float randomRotY = nextFloat(rand, 0, 360);
            Vector3 randomRotation = new Vector3(0, randomRotY, 0);

            // check if there are other objects
            var hitColliders = Physics.OverlapSphere(randomPosition, 0.5f); // second parameter is radius

            breakCouter++;
            if (hitColliders.Length == 1) // for some reason there is always 1 (maybe plane)
            {
                var clone = Instantiate(prefab, randomPosition, Quaternion.Euler(randomRotation));
                clone.transform.SetParent(parent.transform);
                numberPrefabs--;
            }

            if (breakCouter == 5000)
            {
                Debug.LogWarning("Something went wrong, generate  " + prefab.name + " prefabs was unsuccesful. " + numberPrefabs + " prefabs not generated. ");
                break;
            }
        }
    }

    public void destroyPrefab(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }


    public void destroyScene()
    {
        // trees
        destroyPrefab(treeParent);

        // log
        destroyPrefab(logParent);

        // grass
        destroyPrefab(grassParent);

        // rocks
        destroyPrefab(rockParent);

        // bush
        destroyPrefab(bushParent);

        // mushrooms
        destroyPrefab(redMushroomParent);
        destroyPrefab(brownMushroomParent);
    }


}
