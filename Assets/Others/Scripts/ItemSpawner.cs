using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject objectPrefab;
    public GameObject terrainObject;
    public int numObjects = 10;

    void Start()
    {
        if (terrainObject == null)
        {
            Debug.LogError("Terrain reference not set!");
            return;
        }

        for (int i = 0; i < numObjects; i++)
        {
            float randomX = Random.Range(terrainObject.transform.position.x, terrainObject.transform.position.x + terrainObject.transform.localScale.x);
            float randomZ = Random.Range(terrainObject.transform.position.z, terrainObject.transform.position.z + terrainObject.transform.localScale.z);

            Vector3 rayOrigin = new Vector3(randomX, terrainObject.transform.position.y + 100f, randomZ);
            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain")))
            {
                Vector3 spawnPosition = hit.point;
                Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}
