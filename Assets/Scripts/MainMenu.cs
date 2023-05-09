using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int SceneNum;
    public float sceneDelayTime;

    public void Play()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(switchScenceDelay());
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + SceneNum);
    }

    public void Quit()
    {
        StartCoroutine(quitGameDelay());
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //screen switch delay
    IEnumerator switchScenceDelay()
    {
        yield return new WaitForSeconds(sceneDelayTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + SceneNum);

    }

    //wait delay
    IEnumerator quitGameDelay()
    {
        yield return new WaitForSeconds(sceneDelayTime);
        Application.Quit();
        Debug.Log("player has quit the game");
    }

}
