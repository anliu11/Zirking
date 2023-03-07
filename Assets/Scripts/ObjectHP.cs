using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHP : MonoBehaviour
{
    public int objectHP;
    private int wave;
    public int damageMultipler;
    public float destroyWaitTime;

    private bool active = true;

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
            StartCoroutine(destroyStagger());
        }
  
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            damageMultipler += 1;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && damageMultipler >= 0)
        {
            damageMultipler -= 1;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        int damage = 25;
       
        if (collision.gameObject.tag == "Enemy" && active == true)
        {
            active = false;
            objectHP -= damage*damageMultipler;
            StartCoroutine(hitCooldown());
        

            if (objectHP <= 0)
            {
                Instantiate(destroySoundPrefab, transform.position, Quaternion.identity);
                Instantiate(destroyParticlePrefab, transform.position, Quaternion.identity);
                Destroy(parentObject);
            }
        }
    }
    IEnumerator hitCooldown()
    {
        yield return new WaitForSeconds(1f);
        active = true;
    }
    IEnumerator destroyStagger()
    {
        yield return new WaitForSeconds(destroyWaitTime);
        Instantiate(destroySoundPrefab, transform.position, Quaternion.identity);
        Instantiate(destroyParticlePrefab, transform.position, Quaternion.identity);
        Destroy(parentObject);
    }
}