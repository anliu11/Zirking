using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BulletCoutdown());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);   
    }
    IEnumerator BulletCoutdown()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
