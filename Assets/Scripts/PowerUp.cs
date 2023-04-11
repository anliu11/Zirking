using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public float rotationSpeed;
    public PlayerController Player;
   

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Player.GetComponent<PlayerController>().medkitCount < Player.GetComponent<PlayerController>().maxMedkitCount)
        {
            Destroy(gameObject);
        }
    }
}
