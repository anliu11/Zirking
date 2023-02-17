using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class BossScript2 : MonoBehaviour
{
    public GameObject enemyPrefab;
    private int MobNumber;
    public float speed;
    private Rigidbody BossRB;
    public GameObject player;
    public int hitPoints;
    private int playerhP;
    public GameManager gameManager;
    public ParticleSystem damageParticle;
    public ParticleSystem chargeParticle;
    private GameObject spawnManager;
    private int bossalv;
    public GameObject minions;
    private float spawnRangeX = 10;
    private float spawnZMin = 15; // set min spawn Z
    private float spawnZMax = 25; // set max spawn Z
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        BossRB = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    //Spawn position.
    Vector3 GenerateSpawnPosition()
    {
        float yPos = .5f;
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(-spawnZMin, spawnZMax);
        return new Vector3(xPos, yPos, zPos);
    }
    // Update is called once per frame
    public void Update()
    {
        if (gameManager.GetComponent<GameManager>().isGameActive == true)
        {
            //looks at the player
            this.transform.LookAt(player.transform);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            playerhP = player.GetComponent<PlayerController>().hP;
            //getting 
            spawnManager = GameObject.Find("SpawnManager");
            bossalv = spawnManager.GetComponent<SpawnManager>().bossCounter;
            //ability
            while (bossalv == 0)
            {
                
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        int damage = Random.Range(15,25);
        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            hitPoints -= damage;
            damageParticle.Play();
            if (hitPoints <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}  
