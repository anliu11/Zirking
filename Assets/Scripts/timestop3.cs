using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timestop3 : MonoBehaviour
{
    public GameManager gameManager;
    public Vector3 scaleChange;
    public float sphereRadius;
    public float playeralv;
    int layerId = 6;
    public LayerMask player;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << layerId;
        if (gameManager.GetComponent<GameManager>().isGameActive == true)
        {
            transform.localScale += (scaleChange * Time.deltaTime);
            if (Physics.CheckSphere(transform.position, sphereRadius, player))
            {
            Debug.Log("zone");
            }
        }
    }
}
