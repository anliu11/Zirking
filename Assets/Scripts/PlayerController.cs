using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement Variables
    public float moveSpeed;
    public float speedBuff;
    public float speedBuffTime;
    private float moveSpeedPrevious;
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

    // Medkit/Health Variables
    public int medkitCount;
    public int maxMedkitCount;
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

    //for timestop (boss3)
    public GameObject spherebody;
    public bool timezoned2;

    //player stances
    public GameObject idleStance;
    public GameObject shootStance;
    public GameObject healStance;
    public float healStanceTime;
    public bool isGunOut;
    public bool isDonutOut;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        hP = maxHP;
        healthBar.SetMaxHealth(maxHP);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        BossObject = GameObject.Find("Boss Zombie");

        moveSpeedPrevious = moveSpeed;
        isGunOut = true;
        //timestop
        spherebody =  GameObject.Find("Spherebody");
        timezoned2 = false;

    }

    // Update is called once per frame
    void Update()
    {
        //timestop
        Timestopped();

        if (hP <= 0)
        {
            moveSpeed = 0;
        }

        if (gameManager.GetComponent<GameManager>().isGameActive == true)
        {
            // Makes the player look towards the camera
            if (timezoned2 = false)
            {

            }
            else
            {
            Plane playerPlane = new Plane(Vector3.up, transform.position);
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitDist = 0.0f;
            }
            
            // When player presses 2 or F, brings out medkit
            if ((Input.GetKeyDown(KeyCode.Alpha2)) && medkitCount > 0)
            {
                HealStance();

                if (medkitCount > 0)
                {
                    gameManager.donutSilhouette.SetActive(false);
                    gameManager.staffSilhouette.SetActive(true);
                    gameManager.ammoCount.SetActive(false);
                    gameManager.medKitCount.SetActive(true);
                }
            }

            //When player presses 1, brings out gun
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                IdleStance();
                gameManager.staffSilhouette.SetActive(false);
                gameManager.donutSilhouette.SetActive(true);
                gameManager.ammoCount.SetActive(true);
                gameManager.medKitCount.SetActive(false);
            }

            // When player clicks, uses medkit
            if (isDonutOut == true && Input.GetKeyDown(KeyCode.Mouse0))
            {
                StartCoroutine(HealStanceTimer());
                UseMedKit();

                gameManager.staffSilhouette.SetActive(false);
                gameManager.donutSilhouette.SetActive(true);
                gameManager.ammoCount.SetActive(true);
                gameManager.medKitCount.SetActive(false);
            }


            if (timezoned2 = false)
            {

            }
            else (playerPlane.Raycast(ray, out hitDist))
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

            GetComponent<GunShooting>().myInput();
            //GunEffects();

            /*
             // Makes the player shoot
            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<GunShooting>().myInput();
                
                bulletSpawned = Instantiate(bullet.transform, bulletSpawn.transform.position, Quaternion.identity);
                bulletSpawned.rotation = bulletSpawn.transform.rotation;
                playerAudio.PlayOneShot(gunShootSound, 1.0f);
                shootParticle.Play();
                

            }
            */

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
        if (other.CompareTag("MedKit") && medkitCount < maxMedkitCount)
        {
            Debug.Log("Collected MedKit");
            medkitCount += 1;

        }
    }
    void Timestopped()
    {
        timezoned2 = spherebody.GetComponent<timestop2>().timezoned;
        if (timezoned2 == true)
        {
            moveSpeed = 0;
        }
        else
        {
            moveSpeed = 5;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int damage = Random.Range(15, 20);
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
                int damage = Random.Range(25, 30);
                hP -= damage;
                healthBar.SetHealth(hP);

                playerAudio.PlayOneShot(bonk, 0.8f);

                if (hP <= 0)
                {
                    gameManager.PlayBossDeathSound();
                    gameObject.SetActive(false);
                }
            }
            if (BossDps == false)
            {
                int damage = Random.Range(20, 25);
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

    IEnumerator speedCooldown()
    {
        yield return new WaitForSeconds(speedBuffTime);
        moveSpeed = moveSpeedPrevious;
    }

    public void GunEffects()
    {
        playerAudio.PlayOneShot(gunShootSound, 1.0f);
        shootParticle.Play();
    }

    //switches player stances
    public void ShootStance()
    {
        shootStance.gameObject.SetActive(true);
        idleStance.gameObject.SetActive(false);
        healStance.gameObject.SetActive(false);
        isGunOut = true;
        isDonutOut = false;

    }

    public void IdleStance()
    {
        idleStance.gameObject.SetActive(true);
        shootStance.gameObject.SetActive(false);
        healStance.gameObject.SetActive(false);
        isGunOut = true;
        isDonutOut = false;
    }

    public void HealStance()
    {
        healStance.gameObject.SetActive(true);
        shootStance.gameObject.SetActive(false);
        idleStance.gameObject.SetActive(false);

        isGunOut = false;
        isDonutOut = true;
        //StartCoroutine(HealStanceTimer());
    }

    public void UseMedKit()
    {
        medkitCount -= 1;
        hP += 100;
        playerAudio.PlayOneShot(healthKitSound, 0.5f);
        healthBar.SetHealth(hP);
        moveSpeed += speedBuff;
        StartCoroutine(speedCooldown());

        if (medkitCount < 0)
        {
            medkitCount = 0;
        }
    }

    IEnumerator HealStanceTimer()
    {
        yield return new WaitForSeconds(healStanceTime);
    }
}

