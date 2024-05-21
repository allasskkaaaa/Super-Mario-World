using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject[] pickups;

    // Start is called before the first frame update
    void Start()
    {
        SpawnObject();
    }

    void SpawnObject()
    {
        if (pickups.Length == 0)
        {
            Debug.LogError("No pickups assigned to the pickups array.");
            return;
        }

        // Get all child transforms
        List<Transform> childTransforms = new List<Transform>();
        foreach (Transform child in transform)
        {
            childTransforms.Add(child);
        }

        if (childTransforms.Count == 0)
        {
            Debug.LogError("No child objects found to use as spawn locations.");
            return;
        }

        // Instantiate a random pickup to a random child.
        for (int i = 0; i < childTransforms.Count; i++)
        {
            // Select a random pickup prefab
            int randomIndex = Random.Range(0, pickups.Length);
            GameObject randomPickup = pickups[randomIndex];

            //Pick a random child
            int randomChildIndex = Random.Range(0, childTransforms.Count);
            Transform randomChild = childTransforms[randomChildIndex];

            //Spawn Child
            Instantiate(randomPickup, randomChild.position, randomChild.rotation);
        }
        
    }
}
