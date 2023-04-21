using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timestop : MonoBehaviour
{
    public GameManager gameManager;
    public Vector3 scaleChange;
    public LayerMask player;
    public float lifespan; 
    public bool timezoned3;
    public GameObject spherebody;
    

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        spherebody =  GameObject.Find("Spherebody");
        timezoned3 = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetComponent<GameManager>().isGameActive == true)
        {
            timezoned3 = spherebody.GetComponent<timestop2>().timezoned;
            transform.localScale += (scaleChange * Time.deltaTime);
            if (timezoned3 == true)
            {
                lifespan += Time.deltaTime;
                if (lifespan > 3)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
