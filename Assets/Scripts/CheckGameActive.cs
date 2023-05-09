using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGameActive : MonoBehaviour
{
    //variables
    public GameManager gameManager;
    public GameObject checkGameUnActive;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //checks if game is active
        if (gameManager.GetComponent<GameManager>().isGameActive == true)
        {
            //sets the game unactive checker on
            checkGameUnActive.SetActive(true);
        }
    }
}
