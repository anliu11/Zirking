using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript3 : MonoBehaviour
{
    private int playerhP;
    private Rigidbody BossRB;
    

    public float abilitytimer;
    public float abilitytimer2;
    public float abilitytimer3;
    public float speed;
    public int hitPoints;
    public bool timezoned4;
    public GameObject player;
    public GameObject aura;
    public GameObject spherebody;
    public GameManager gameManager;
    public ParticleSystem damageParticle;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        BossRB = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        speed = 5;
        timezoned4 = false;
        spherebody =  GameObject.Find("Spherebody");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetComponent<GameManager>().isGameActive == true)
        {
            //looks at the player
            timezoned4 = spherebody.GetComponent<timestop2>().timezoned;
            this.transform.LookAt(player.transform);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            playerhP = player.GetComponent<PlayerController>().hP;
            abilitytimer += Time.deltaTime;
            if (abilitytimer > 6.0f)
            {
                speed = 0;
                abilitytimer2 += Time.deltaTime;
                if (abilitytimer2 > 2.0f)
                {
                    Instantiate(aura, generatesspawnpos(), aura.transform.rotation);
                    abilitytimer3 += Time.deltaTime;
                    if (abilitytimer3 > 1)
                    {
                    if (timezoned4 == true)
                    {
                        speed = 10;
                        abilitytimer2 = 0;
                        abilitytimer = -1;
                        abilitytimer3 = 0
                    }   
                    else
                    {
                        speed = 5;
                        abilitytimer2 = 0;
                        abilitytimer = -1;
                        abilitytimer3 = 0
                    }
                    }
                }

            }
        }

    }
    Vector3 generatesspawnpos()
    {
        float yPos = .5f;
        float xPos = transform.position.x;
        float zPos = transform.position.z;
        return new Vector3(xPos,yPos,zPos);
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
