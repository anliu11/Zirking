using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI waveNumber;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI enemyCount;
    private GameObject spawnManager;
    private GameObject player;
    private int wave;
    private int playerHP;
    public Button restartButtonLose;
    public Button returnButtonMain;
    public Button returnButtonLose;
    public GameObject titleScreen;
    public GameObject playerHud;
    public GameObject healthBar;
    public GameObject winScreen;
    public bool isGameActive = false;
    private float enemyNum;
    public float winWaveNum;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Updates the wave number by getting it from other script.
        wave = spawnManager.GetComponent<SpawnManager>().waveCount - 1;
        waveNumber.text = "Wave: " + wave.ToString();
        enemyNum = spawnManager.GetComponent<SpawnManager>().enemyCount + spawnManager.GetComponent<SpawnManager>().bossCounter;
        enemyCount.text = "Enemy Count: " + enemyNum.ToString();



        //Runs when the player dies.
        playerHP = player.GetComponent<PlayerController>().hP;
        if (playerHP <= 0)
        {
            GameOver();
        }

        //Runs when player reaches level x
        if (wave > winWaveNum)
        {
            Win();
        }
    }

    public void UpdateWave(int waveToAdd)
    {
        wave += waveToAdd;
    }

    //Game win code
    public void Win()
    {
        isGameActive = false;
        winScreen.gameObject.SetActive(true);
        Debug.Log("Player has won");
    }

    //Game over code.
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButtonLose.gameObject.SetActive(true);
        returnButtonLose.gameObject.SetActive(true);
        isGameActive = false;
        Debug.Log("Player Died");
    }

    // Runs when the restart button is pressed.
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Runs when any of the starting buttons are pressed.
    public void StartGame()
    {
        isGameActive = true;
        Debug.Log("isGameActive is true");
        spawnManager = GameObject.Find("SpawnManager");
        wave = spawnManager.GetComponent<SpawnManager>().waveCount - 1;
        waveNumber.text = "Wave: " + wave.ToString();

        enemyNum = spawnManager.GetComponent<SpawnManager>().enemyCount + spawnManager.GetComponent<SpawnManager>().bossCounter;
        enemyCount.text = "Enemy Count: " + enemyNum.ToString(); 


        player = GameObject.Find("Player");
        titleScreen.gameObject.SetActive(false);
        returnButtonMain.gameObject.SetActive(false);
        playerHud.gameObject.SetActive(true);
        healthBar.SetActive(true);
    }
}
