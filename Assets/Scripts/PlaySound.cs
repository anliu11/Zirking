using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{

    public AudioSource soundPlayer;
    private bool setAudioSourceInstance = false;
    public AudioClip clickSound;

    // Start is called before the first frame update
    void Start()
    {
        soundPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(setAudioSourceInstance == false)
        {
            soundPlayer = GetComponent<AudioSource>();
            setAudioSourceInstance = true;
            Debug.Log("Effects Audio Source is set");
        }
        */
    }

    // Plays sound effect when called
    public void Play()
    {
        soundPlayer.Play();
        Debug.Log("button click noise has played");

    }

    public void PlayButtonClick()
    {
        soundPlayer.Stop();
        soundPlayer.clip = clickSound;
        soundPlayer.Play();
        Debug.Log("button click noise has played");

        soundPlayer.PlayOneShot(clickSound, 1);
        Debug.Log("button click noise has played");

    }


}
