using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGameUnActive : MonoBehaviour
{
    //variables
    public GameManager gameManager;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //checks if game is unactive
        if (gameManager.GetComponent<GameManager>().isGameActive == false)
        {
            //sets the player to inactive
            player.SetActive(false);
            gameManager.PlayBossDeathSound();
        }
    }
}
