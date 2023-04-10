using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement Variables
    public float moveSpeed;
    private Vector3 moveInput;
    private Vector3 moveVelocity;

    private float zBound = 30.0f;
    public float waitTime;
    private Rigidbody playerRb;
    public GameObject playerObj;
    public GameObject bulletSpawn;
    public GameObject bullet;
    public Camera camera;
    private Transform bulletSpawned;
    public ParticleSystem shootParticle;
    public int hP;
    private int maxHP = 100;
    public HealthBar healthBar;
    public GameManager gameManager;
    public GameObject BossObject;
    public bool BossDps;
    private AudioSource playerAudio;
    public AudioClip gunShootSound;
    public AudioClip healthKitSound;
    public AudioClip bonk;
    public bool abilityboss3;
    public GameObject spherebody;
    public bool timezoned2;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        hP = maxHP;
        healthBar.SetMaxHealth(maxHP);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        BossObject = GameObject.Find("Boss Zombie");
        abilityboss3 = gameManager.GetComponent<GameManager>().vampireability;
        spherebody =  GameObject.Find("Spherebody");

   
    }

    // Update is called once per frame
    void Update()
    {
        timezoned2 = spherebody.GetComponent<Timestop2>().timezoned;
        if (hP <= 0)
        {
            moveSpeed = 0;
        }
        if (abilityboss3 == true)
        {
            moveSpeed = 0;
        }
        else
        {
            moveSpeed = 5;
        }
        if (gameManager.GetComponent<GameManager>().isGameActive == true)
        {
            // Makes the player look towards the camera
            Plane playerPlane = new Plane(Vector3.up, transform.position);
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitDist = 0.0f;

            if (playerPlane.Raycast(ray, out hitDist))
            {
                Vector3 targetPoint = ray.GetPoint(hitDist);
                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
                targetRotation.x = 0;
                targetRotation.z = 0;
                playerObj.transform.rotation = Quaternion.Slerp(playerObj.transform.rotation, targetRotation, 7f * Time.deltaTime);
            }



            //========================================================================

            MovePlayer();
            Boundries();

            // Makes the player shoot
            if (Input.GetMouseButtonDown(0))
            {
                bulletSpawned = Instantiate(bullet.transform, bulletSpawn.transform.position, Quaternion.identity);
                bulletSpawned.rotation = bulletSpawn.transform.rotation;
                playerAudio.PlayOneShot(gunShootSound, 1.0f);
                shootParticle.Play();
                

            }
            //========================================================================

            //Set MaxHP
            if (hP > maxHP)
            {
                hP = maxHP;
            }
            BossDps = BossObject.GetComponent<BossScript>().bossdps;
        }
        
    }
    // Part of player movement system
    void FixedUpdate()
    {
        playerRb.velocity = moveVelocity;
    }

    // Moves the player based on WASD or arrow keys
    void MovePlayer()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * moveSpeed;
    }

    //Top and  bottom boundries.
    void Boundries()
    {
        if (transform.position.z < -zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);
        }

        if (transform.position.z > zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
        }
    }
    //Kills player when hp = 0 //When Collecting MedicKit Heal 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MedKit"))
        {
            Debug.Log("Collected MedKit");
            hP += 100;
            playerAudio.PlayOneShot(healthKitSound, 0.5f);
            healthBar.SetHealth(hP);

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int damage = Random.Range(15,20);
            Debug.Log("Collided with " + collision.gameObject.name);
            hP -= damage;
            healthBar.SetHealth(hP);
            Rigidbody enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayyFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRigidBody.AddForce(awayyFromPlayer * 5, ForceMode.Impulse);

            playerAudio.PlayOneShot(bonk, 0.8f);

            if (hP <= 0)
            {
                gameManager.PlayDeathSound();
                gameObject.SetActive(false);
            }
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            Debug.Log("Collided with " + collision.gameObject.name);
            if (BossDps == true)
            {
                int damage = Random.Range(25,30);
                hP -= damage;
                healthBar.SetHealth(hP);

                playerAudio.PlayOneShot(bonk, 0.8f);

                if (hP <= 0)
                {
                    gameManager.PlayBossDeathSound();
                    gameObject.SetActive(false);
                }
            }
            if (collision.gameObject.CompareTag("aura"))
            {
                moveSpeed = 0;
            }
            if (BossDps == false)
            {
                int damage = Random.Range(20,25);
                hP -= damage;
                healthBar.SetHealth(hP);

                playerAudio.PlayOneShot(bonk, 0.8f);


                if (hP <= 0)
                {
                    gameManager.PlayBossDeathSound();
                    gameObject.SetActive(false);
                }
            }

        }
    }
}

