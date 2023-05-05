using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGmenuMusic : MonoBehaviour
{
    //variables
    public AudioClip menuMusic;
    public AudioClip tipsMusic;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //plays normal menu music
    public void PlayMenuMusic()
    {
        audioSource.Stop();
        audioSource.clip = menuMusic;
        audioSource.Play();
    }


    //plays tips music
    public void PlayTipsMusic()
    {
        audioSource.Stop();
        audioSource.clip = tipsMusic;
        audioSource.Play();
    }
}
