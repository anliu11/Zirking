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
    public int bossalv;
    public float elapsedtime = 0.0f;
    public GameObject spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        enemyRB = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        spawnManager = GameObject.Find("SpawnManager");
       
    }

    // Update is called once per frame
    void Update()
    {
        bossalv = spawnManager.GetComponent<SpawnManager>().bossCounter;
        //looks at the player
        if (gameManager.GetComponent<GameManager>().isGameActive == true)
        {
            if (bossalv == 1)
            {
                this.transform.LookAt(player.transform);
                speed = 0;
                elapsedtime += Time.deltaTime;
                if (elapsedtime > 1)
                {
                    speed = 5;
                    transform.Translate(Vector3.forward * Time.deltaTime * speed);
                }
            }
            else
            {
                this.transform.LookAt(player.transform);
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }

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
        speed = 5;
    }

    public void StartGame()
    {

    }
}

