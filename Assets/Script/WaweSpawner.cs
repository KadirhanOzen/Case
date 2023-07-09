using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaweSpawner : MonoBehaviour
{
    [SerializeField]
    public Transform[] spawnPoints;

    [SerializeField]
    List<GameObject> enemyPrefabs = new List<GameObject>();
    float spawnInterval;


    private void Start() 
    {   
        StartCoroutine(SpawnWave());
        
    }

    IEnumerator SpawnWave()
    {

        while (true)
        {
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length);

            int minute = Mathf.CeilToInt((Time.time + 0.1f - GameManager.instance.startTime)/60);

            spawnInterval = 2 / Mathf.Pow(minute, 1.7f) / 2 + 2 + (Mathf.Sin(minute) / 2);        

            yield return new WaitForSeconds(spawnInterval);

            int randomEnemyType =  Random.Range(0,2);

            Instantiate(enemyPrefabs[randomEnemyType], spawnPoints[randomSpawnPoint].position, Quaternion.identity);

            


           

        }


    }






}


