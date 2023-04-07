using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public GameObject medpackPrefab;

    private float spawnRangeX = 10;
    private float spawnZMin = 15; // set min spawn Z
    private float spawnZMax = 25; // set max spawn Z

    public int waveCount = 1;
    public int bossCount;
    public int bossCounter;
    public int totalEnimies;
    public int enemyCount;


    // Start is called before the first frame update
    void Start()
    {
        bossCounter = 0;
    }

    //Creates a spawn position for a zombie.
    Vector3 GenerateSpawnPosition()
    {
        float yPos = .5f;
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(-spawnZMin, spawnZMax);
        return new Vector3(xPos, yPos, zPos);
    }
    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        bossCounter = GameObject.FindGameObjectsWithTag("Boss").Length;
        totalEnimies = enemyCount + bossCounter;
        if  (totalEnimies == 0)
        {
            spawnWave(waveCount);
            waveCount += 1;
        }
    }
    void spawnWave(int waveCount)
    {
        for (int i = 0; i < waveCount; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
            
        }
        if (GameObject.FindGameObjectsWithTag("MedKit").Length == 0)
        {
            Instantiate(medpackPrefab, GenerateSpawnPosition(), medpackPrefab.transform.rotation);
        }
        if (waveCount % 10 == 0)
        {
            for (int c = 0; c < bossCount; c++)
            {
                bossCounter += 1;
                Instantiate(bossPrefab, GenerateSpawnPosition(), bossPrefab.transform.rotation);
            }
            bossCount += 1; 
        }
    }
}