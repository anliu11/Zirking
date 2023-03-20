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
    public float delay = 0;

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
        if (waveCount == 10 && totalEnimies == 0)
        {
            Instantiate(bossPrefab, GenerateSpawnPosition(), bossPrefab.transform.rotation);
            waveCount += 1;
        }
        if (totalEnimies == 0 && waveCount < 10)
        {
            spawnWave(waveCount);
            waveCount += 1;
        }
        if (waveCount == 11)
        {
            delay += Time.deltaTime;
            if (delay > 5 && totalEnimies == 0)
            {
                waveCount += 1;
                delay = 0;
            }
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
    }
}