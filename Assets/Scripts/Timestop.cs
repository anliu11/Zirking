using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Timestop : MonoBehaviour
{
    public GameManager gameManager;
    public Vector3 scaleChange;
    public GameObject player;
    public float speed;
    private Rigidbody BossRB;

    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
        BossRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetComponent<GameManager>().isGameActive == true)
        {
            transform.localScale += (scaleChange * Time.deltaTime);
        }
    }
}
