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
    private float spawnRangeX = 10;
    private float spawnZMin = 5; // set min spawn Z
    private float spawnZMax = 10; // set max spawn Z 
    
    // Start is called before the first frame update
    class Global
    {
        public float posx;
        public float posz;
    }
    void Start()
    {
        player = GameObject.Find("Player");
        BossRB = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        float posx = GameObject.Find("boss skeleton").transform.position.x;
        float posz = GameObject.Find("boss skeleton").transform.position.z;
        InvokeRepeating("Ability", 1, 12);
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
        }
        while (GameObject.FindGameObjectsWithTag("Boss").Length) > 0 
        {
            Ability()
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
    void Ability()
    {
        speed = 0;
        chargeParticle.Play();
        MobNumber = Random.Range(3,5);
        StartCoroutine(BuildUp(MobNumber));
        StartCoroutine(AbilityTimer());
    }
    Vector3 GenerateSpawnPosition()
    {
        float yPos = .5f;
        float xPos = posx + Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = posz + Random.Range(-spawnZMin, spawnZMax);
        return new Vector3(xPos, yPos, zPos);
    }
    IEnumerator BuildUp(int MobNumber)
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < MobNumber; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
        speed = 2.5f;
    }
    IEnumerator AbilityTimer()
    {
        yield return new WaitForSeconds(4);
    }
}  
