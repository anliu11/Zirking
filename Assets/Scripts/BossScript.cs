using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class BossScript : MonoBehaviour
{

    public float speed;
    private Rigidbody BossRB;
    public GameObject player;
    public TrailRenderer dashTrail;
    public int hitPoints;
    private int playerhP;
    public GameManager gameManager;
    public bool bossdps;
    public ParticleSystem damageParticle;
    public ParticleSystem chargeParticle;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        BossRB = GetComponent<Rigidbody>();
        InvokeRepeating("Ability", 4, 6);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        bossdps = false;
        dashTrail = GetComponent<TrailRenderer>();

        healthBar.SetMaxHealth(hitPoints);

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
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        int damage = Random.Range(15,25);
        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            healthBar.SetHealth(hitPoints);
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
        if (gameManager.GetComponent<GameManager>().isGameActive == true)
        {
            speed = 0;
            chargeParticle.Play();
            dashTrail.enabled = true;
            StartCoroutine(BuildUp());
            StartCoroutine(AbilityTimer());
        }
    }
    IEnumerator BuildUp()
    {
        yield return new WaitForSeconds(2);
        bossdps = true;
        speed = 2.5f;
        BossRB.AddForce(transform.forward * 60, ForceMode.Impulse);
    }
    IEnumerator AbilityTimer()
    {
        yield return new WaitForSeconds(4);
        bossdps = false;
        dashTrail.enabled = false;
    }
}  
