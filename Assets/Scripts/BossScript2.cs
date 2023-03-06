using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class BossScript2 : MonoBehaviour
{

    public float speed;
    private Rigidbody BossRB;
    public GameObject player;
    public GameObject enemyPrefab;
    public int hitPoints;
    private int playerhP;
    public float spawnTimer = 5;
    public float pauseboss;
    public float elapsedtime;
    public int bossalv;
    public int minionsalv;
    public GameManager gameManager;
    public GameObject spawnManager;
    public ParticleSystem damageParticle;
    public ParticleSystem chargeParticle;

    private float spawnMinX = -15;
    private float spawnMaxX = 17;
    private float spawnZMin = -15; // set min spawn Z
    private float spawnZMax = 25; // set max spawn Z
    private float spawnRadius = 5;

    // Returns true if the point is in the bounding box defined by the x and z min/max
    bool CheckBoundingBox(Vector3 point, float minX, float maxX, float minZ, float maxZ)
    {
        return minX < point.x && point.x < maxX && minZ < point.z && point.z < maxZ;
    }

    // Returns true if the point is within the radius of the Boss's position
    bool CheckBoundingRadius(Vector3 point, float radius)
    {
        return Vector3.Distance(point, transform.position) < radius;
    }

    // Repeatedly generates a spawn position until it is within a specific
    // radius of the boss prefab and within the bounding box of the "level"
    Vector3 GenerateRadialSpawnPosition()
    {
        Vector3 point = GenerateSpawnPosition();
        Debug.Log("Point: " + point);
        while (!(CheckBoundingBox(point, spawnMinX, spawnMaxX, spawnZMin, spawnZMax)
                && CheckBoundingRadius(point, spawnRadius)))
        {
            point = GenerateSpawnPosition();
            Debug.Log("while Point: " + point);
        }
        return point;
    }

    //Creates a spawn position for a zombie.
    Vector3 GenerateSpawnPosition()
    {
        float yPos = .5f;
        float xPos = Random.Range(spawnMinX, spawnMaxX);
        float zPos = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(xPos, yPos, zPos);
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        BossRB = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetComponent<GameManager>().isGameActive == true)
        {
            //looks at the player
            this.transform.LookAt(player.transform);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            playerhP = player.GetComponent<PlayerController>().hP;
            spawnManager = GameObject.Find("SpawnManager");
            bossalv = spawnManager.GetComponent<SpawnManager>().bossCounter;
            minionsalv = spawnManager.GetComponent<SpawnManager>().enemyCount;
            if (bossalv == 1)
            {
                elapsedtime += Time.deltaTime;
                if (elapsedtime > spawnTimer)
                {
                    elapsedtime = 0;
                    Debug.Log("working");
                    int minionamount = Random.Range(2,3);
                    int i = 0;
                    speed = 0;
                    pauseboss += Time.deltaTime;
                    while (i <= minionamount)
                    {
                        Instantiate(enemyPrefab, GenerateRadialSpawnPosition(), enemyPrefab.transform.rotation);
                        i += 1;
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