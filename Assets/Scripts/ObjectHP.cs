using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHP : MonoBehaviour
{
    public int objectHP;
    private int wave;
    public int bounceMultiplier;
    public GameObject parentObject;
    public GameObject destroySoundPrefab;
    public GameObject destroyParticlePrefab;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetComponent<GameManager>().wave == 10)
        {
            Instantiate(destroySoundPrefab, transform.position, Quaternion.identity);
            Instantiate(destroyParticlePrefab, transform.position, Quaternion.identity);

            Destroy(parentObject);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        int damage = 20;
        Rigidbody enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 awayyFromPlayer = collision.gameObject.transform.position - transform.position;

        if (collision.gameObject.tag == "Enemy")
        {
            enemyRigidBody.AddForce(awayyFromPlayer * bounceMultiplier, ForceMode.Impulse);
            Debug.Log("Object was hit");
            objectHP -= damage;

            if (objectHP <= 0)
            {
                Instantiate(destroySoundPrefab, transform.position, Quaternion.identity);
                Instantiate(destroyParticlePrefab, transform.position, Quaternion.identity);
               
                Destroy(parentObject);
              
            }
    
        }
    }
}
