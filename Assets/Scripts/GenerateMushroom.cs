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

    // IMPORTANT! do not set too small or else while condition will never end
    public int distanceX;
    public int distanceZ;

    public int redMushrooms = 0;
    public int brownMushrooms = 0;
    private Random rand = new Random();

    // Start is called before the first frame update
    void Start()
    {
        generateMushrooms(redMushrooms, redMushroomStumpParent, redMushroomStumpPreFab);
        generateMushrooms(brownMushrooms, brownMushroomStumpParent, brownMushroomStumpPreFab);

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
            // Debug.Log("position: " + randomPosition);

            // check if there are other objects
            var hitColliders = Physics.OverlapSphere(randomPosition, 2); // second parameter is radius

            if (hitColliders.Length == 1) // for some reason there is always 1 (maybe plane)
            {
                var mushroomClone = Instantiate(prefab, randomPosition, Quaternion.identity);
                mushroomClone.transform.SetParent(parent.transform);
                // Debug.Log("Success!");
                counter--;
            }
        }
    }

    public void regenerateMushrooms()
    {
        
        foreach (Transform child in redMushroomStumpParent.transform)
        {
            // TODO: check if this is the right way
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in brownMushroomStumpParent.transform)
        {
            // TODO: check if this is the right way
            GameObject.Destroy(child.gameObject);
        }
        generateMushrooms(redMushrooms, redMushroomStumpParent, redMushroomStumpPreFab);
        generateMushrooms(brownMushrooms, brownMushroomStumpParent, brownMushroomStumpPreFab);

    
    }


}
