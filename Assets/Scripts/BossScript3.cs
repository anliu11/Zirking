using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class BossScript3 : MonoBehaviour
{
    private int playerhP;
    private Rigidbody BossRB;

    public bool abilityon = false;
    public float abilitytimer;
    public float abilityusagetime;
    public float speed;
    public int hitPoints;
    public GameObject player;
    public GameManager gameManager;
    public ParticleSystem damageParticle;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        BossRB = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        abilityon = gameManager.GetComponent<GameManager>().vampireability;
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
            abilitytimer += Time.deltaTime;
            if (abilitytimer > 5)
            {
                abilityusagetime += Time.deltaTime;
                abilityon = true;
                if (abilityusagetime > 3)
                {
                    abilityusagetime = 0;
                    abilitytimer = 0;
                    abilityon = false;
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
