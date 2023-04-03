using System.Collections;
using System.Collections.Generic;
using UnityEngine;

   public GameManager gameManager;

public class Timestop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetComponent<GameManager>().isGameActive == true)
        {
            
        }
    }
}
