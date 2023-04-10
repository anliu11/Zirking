using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class Timestop2 : MonoBehaviour
{
    public GameManager gameManager;
    public float sphereRadius;
    public float playeralv;
    int layerId = 6;
    public LayerMask player;
    public bool timezoned;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        timezoned = false; 
    }
    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << layerId;
        if (gameManager.GetComponent<GameManager>().isGameActive == true)
        {
            if (Physics.CheckSphere(transform.position, sphereRadius, player))
            {
            Debug.Log("zone");
            timezoned = true;
            }
        }
    }
}  
