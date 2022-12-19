using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GenerateMushroom : MonoBehaviour
{
    public GameObject redMushroomStumpParent;
    public GameObject redMushroomStumpPreFab;
    public GameObject brownMushroomStumpParent;
    public GameObject brownMushroomStumpPreFab;

    public int distanceX;
    public int distanceZ;

    public int redMushrooms;
    public int brownMushrooms;
    private Random rand = new Random();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");

        generateMushrooms(redMushrooms, redMushroomStumpParent, redMushroomStumpPreFab);
        generateMushrooms(brownMushrooms, brownMushroomStumpParent, redMushroomStumpPreFab);

        Debug.Log("done");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private float nextFloat(Random rnd, double minValue, double maxValue)
    {
        return (float)( rnd.NextDouble() * (maxValue - minValue) + minValue);
    }

    private void generateMushrooms(int counter, GameObject parent, GameObject prefab)
    {
        while (counter != 0)
        {

            // create random positions
            float randomX = nextFloat(rand, -distanceX, distanceX);
            float randomZ = nextFloat(rand, -distanceZ, distanceZ);
            Vector3 randomPosition = new Vector3(randomX, 0, randomZ);
            Debug.Log("position: " + randomPosition);

            // check if there are other objects
            var hitColliders = Physics.OverlapSphere(randomPosition, 0.5f); // second parameter is radius

            if (hitColliders.Length == 1) // for some reason there is always 1 (maybe plane)
            {
                var mushroomClone = Instantiate(prefab, randomPosition, Quaternion.identity);
                mushroomClone.transform.SetParent(parent.transform);
                Debug.Log("Success!");
                counter--;
            }
        }
    }
}
