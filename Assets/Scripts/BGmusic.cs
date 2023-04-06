using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGmusic : MonoBehaviour
{
    //variables
    public GameManager gameManager;
    private int waveNum;
    public AudioClip bossMusic;
    private AudioSource audioSource;
    private bool bossWaveActive = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        waveNum = gameManager.GetComponent<GameManager>().wave;

        //if(waveNum == 10 && bossWaveActive == false)
        //{
        //    audioSource.Stop();
        //    audioSource.clip = bossMusic;
        //    audioSource.Play();
            //audioSource.PlayOneShot(bossMusic, 2.5f);
        //    bossWaveActive = true;
        //}
    }
}
