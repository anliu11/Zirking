using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHP : MonoBehaviour
{
    public int objectHP;
    public GameObject parentObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter(Collision collision)
    {
        int damage = 20;
        Rigidbody enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 awayyFromPlayer = collision.gameObject.transform.position - transform.position;

        if (collision.gameObject.tag == "Enemy")
        {
            enemyRigidBody.AddForce(awayyFromPlayer * 3, ForceMode.Impulse);
            Debug.Log("Object was hit");
            objectHP -= damage;

            if (objectHP <= 0)
            {
                Destroy(parentObject);
            }
        }
    }
}
