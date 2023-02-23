using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class BossScript2 : MonoBehaviour
{
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
    private float spawnRangeX = 5;
    private float spawnZMin = 5; // set min spawn Z
    private float spawnZMax = 10; // set max spawn Z
    private float spawnTimer = 2;
    public float elapsedtime;
    private float spawnboundx = 10;
    private float spawnboundzmin = 15;
    private float spawnboundzmax = 25;
    private float sKpx;
    private float sKpz;
    private gameObject mob;
    // Start is called before the first frame update
    void Start()
    {
        //getting object skelly lol so that we can get its cords.
        mob = GameObject.Find("skeleton 1(Clone)");
        player = GameObject.Find("Player");
        BossRB = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    //Spawn position.
    Vector3 GenerateSpawnPosition()
    {
        float yPos = .5f;
        float xPos = transform.position.x + Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = transform.position.z + Random.Range(-spawnZMin, spawnZMax);
        return new Vector3(xPos, yPos, zPos);
    }
    // Update is called once per frame
    public void Update()
    {
        //find the spawned enemy position ig.
        if (gameManager.GetComponent<GameManager>().isGameActive == true)
        {
            //looks at the player
            this.transform.LookAt(player.transform);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            playerhP = player.GetComponent<PlayerController>().hP;
            //getting number from spawnManger to see if boss is on the field.
            spawnManager = GameObject.Find("SpawnManager");
            bossalv = spawnManager.GetComponent<SpawnManager>().bossCounter;
            //ability if the boss is on the field
            if (bossalv == 1)
            {
                elapsedtime += Time.deltaTime;
                if (elapsedtime > spawnTimer)
                {
                    elapsedtime = 0;
                    Debug.Log("working");
                    int minionamount = Random.Range(2,3);
                    int i = 0;
                    while (i <= minionamount)
                    {
                        Instantiate(minions, GenerateSpawnPosition(), minions.transform.rotation);
                        i += 1;
                        //try to make it find skeleton position check if its out of bounds, delete and remove 1 from count.
                        /*if (gameObject.Find("skeleton 1"))
                        {

                        }
                        */
                    }
                    
                }
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
