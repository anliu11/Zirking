using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timestop : MonoBehaviour
{
    public GameManager gameManager;
    public Vector3 scaleChange;
    public LayerMask player;

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
            transform.localScale += (scaleChange * Time.deltaTime);
        }
    }
}
