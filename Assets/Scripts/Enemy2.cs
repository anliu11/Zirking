using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy2 : MonoBehaviour
{
    private int damage;
    public float speed;
    private Rigidbody enemyRB;
    public GameObject player;
    public int hitPoints;
    public GameManager gameManager;
    public ParticleSystem damageParticle;
    private float spawnRangeX = 10;
    private float spawnZMin = 15; // set min spawn Z
    private float spawnZMax = 25; // set max spawn Z
    // part of finding bounds system
    private float sKpx;
    private float sKpz;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        enemyRB = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        sKpx = transform.position.x;
        sKpz = transform.position.z;
        
        //--trying to make it so that the enemy delets itself if it spawns out of bounds--
        if (((sKpx < -spawnRangeX || sKpx > spawnRangeX) && (sKpz < spawnZMin || sKpz > spawnZMax)))
        {
            Destroy(gameObject);
        }
        /*
        //type two works.
        if (sKpx < -spawnRangeX)
        {
            if (sKpx > spawnRangeX)
            {
                Destroy(gameObject);
            }
        }
        if (sKpz < spawnZMin)
        {
            if (sKpz > spawnZMax)
            {
            Destroy(gameObject);
            }
        }
        */
        //looks at the player
        if (gameManager.GetComponent<GameManager>().isGameActive == true)
        {
            this.transform.LookAt(player.transform);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        int damage = Random.Range(10,15);
        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            hitPoints -= damage;
            damageParticle.Play();
            if (hitPoints <= 0)
            {
                Destroy(gameObject);
            }
            speed = 0;
            StartCoroutine(staggeredUnit());

        }
    }
    IEnumerator staggeredUnit()
    {
        yield return new WaitForSeconds(0.1f);
        speed = 3;
    }

    public void StartGame()
    {

    }
}

