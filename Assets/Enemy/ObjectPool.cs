using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int poolSize = 5;
    [SerializeField] float spawnTimer = 1f;
    

    GameObject[] pool;
    //List<Transform> spawnPoints = new List<Transform>(); // List to hold spawn points
    //int currentSpawnIndex = 0; //add

   void Awake()
    {
        PopulatePool();
       // FindSpawnPoints();  // Find spawn points based on tag
    }
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }
    //// Find spawn points by tag "Path" (you can filter spawn points here if needed)
    //void FindSpawnPoints()
    //{
    //    GameObject[] spawnObjects = GameObject.FindGameObjectsWithTag("Path");
    //    foreach (GameObject spawn in spawnObjects)
    //    {
    //        // If spawn object is a spawn point (e.g., the first one or based on specific logic):
    //        spawnPoints.Add(spawn.transform); // Add spawn point to the list
    //    }
    //}
    void EnableObjectInPool()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }
                    

               

   


    //private Vector3 GetStartPosition()
    //{
    //    // Assuming your path starts at the first waypoint
    //    Waypoint[] waypoints = FindObjectsOfType<Waypoint>();
    //    foreach (Waypoint waypoint in waypoints)
    //    {
    //        if (waypoint.CompareTag("Path"))
    //        {
    //            return waypoint.transform.position;
    //        }
    //    }
    //    Debug.LogWarning("Start position not found. Make sure waypoints are tagged correctly.");
    //    return transform.position; // Fallback position
    //}

    IEnumerator SpawnEnemy()
    {
        while(true)
        {
           EnableObjectInPool();
          
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
