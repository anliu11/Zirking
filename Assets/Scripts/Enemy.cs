using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private int damage;
    public float speed;
    private Rigidbody enemyRB;
    public GameObject player;
    public int hitPoints;
    public GameManager gameManager;
    public ParticleSystem damageParticle;

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
        //looks at the player
        if (gameManager.GetComponent<GameManager>().isGameActive == true)
        {
            this.transform.LookAt(player.transform);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject && hitPoints <= 0)
        {
            gameObject.transform.position = new Vector3(100, 0, 100);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        int damage = Random.Range(15, 25);
        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            hitPoints -= damage;
            damageParticle.Play();
            if (hitPoints <= 0)
            {
                StartCoroutine(waitUntilDestroy());

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
    IEnumerator waitUntilDestroy()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
    public void StartGame()
    {

    }
}

