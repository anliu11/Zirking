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
    public int wave;
    private int playerHP;
    public Button restartButtonLose;
    public Button returnButtonMain;
    public Button returnButtonLose;
    public Button returnButtonWin;
    public GameObject titleScreen;
    public GameObject playerHud;
    public GameObject healthBar;
    public GameObject winScreen;
    public bool isGameActive = false;
    private float enemyNum;
    public float winWaveNum;
    private AudioSource gameManagerAudio;
    public AudioClip deathSound;
    public AudioClip bossDeathSound;
    private bool deathInstance = false;
    private GameObject cursorManeger;
    public bool waveDestroy = true;
    public GameObject waveDestroySound;

    public int bossCount;

    //Health UI
    [SerializeField] private CanvasGroup HealthBarGroup;
    public bool FadeInHP = true;
    public bool FadeOutHP = true;

    //Inventory UI
    public GameObject inventoryUI;
    public GameObject staffSilhouette;
    public GameObject donutSilhouette;
    public GameObject ammoCount;
    public GameObject medKitCount;
    public TextMeshProUGUI ammoCountText;
    public TextMeshProUGUI medkitCountText;

    public int medkitnum;
    public int maxmedkitnum;

    //Restart Scene Delay Num
    public float RestartSceneDelayTime;

    // Start is called before the first frame update
    void Start()
    {
        HealthBarGroup.alpha = 0;
        gameManagerAudio = GetComponent<AudioSource>();
        cursorManeger = GameObject.Find("Cursor Maneger");

        cursorManeger.GetComponent<CursorManeger>().PointerCursor();

    }

    // Update is called once per frame
    void Update()
    {
        //Updates the wave number by getting it from other script.
        wave = spawnManager.GetComponent<SpawnManager>().waveCount - 1;
        waveNumber.text = "WAVE- " + wave.ToString() + " OF 10";
        enemyNum = spawnManager.GetComponent<SpawnManager>().enemyCount + spawnManager.GetComponent<SpawnManager>().bossCounter;
        enemyCount.text = "ENEMY COUNT- " + enemyNum.ToString();

        bossCount = GameObject.FindGameObjectsWithTag("Boss").Length;

        medkitnum = player.GetComponent<PlayerController>().medkitCount;
        maxmedkitnum = player.GetComponent<PlayerController>().maxMedkitCount;
        medkitCountText.text = medkitnum.ToString() + "/" + maxmedkitnum.ToString();

        //Runs when the player dies.
        playerHP = player.GetComponent<PlayerController>().hP;
        if (playerHP <= 0)
        {
            GameOver();

            //if (playerHP <= 0 && deathInstance == false)
            //{
            //    gameManagerAudio.Stop();
            //    gameManagerAudio.clip = deathSound;
            //    gameManagerAudio.Play();
            //gameManagerAudio.PlayOneShot(deathSound, 0.3f);
            //    deathInstance = true;
            //}
        }

        //Runs when player reaches level x
        if (wave > winWaveNum)
        {
            Win();
        }
        if (wave == 10 && waveDestroy == true)
        {

            waveDestroy = false;
            Instantiate(waveDestroySound, transform.position, Quaternion.identity);
            playerHud.SetActive(false);
        }
        if (bossCount == 1)
        {
            if (FadeInHP == true)
            {
                HealthBarGroup.alpha += Time.deltaTime;

                if (HealthBarGroup.alpha >= 1)
                {
                    FadeInHP = false;
                    bossCount = 0;

                }
            }
        }
        if (bossCount == 0)
        {
                HealthBarGroup.alpha -= Time.deltaTime;
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
        AllHudOff();
        winScreen.gameObject.SetActive(true);
        returnButtonWin.gameObject.SetActive(true);
        Debug.Log("Player has won");

        player.SetActive(false);
        cursorManeger.GetComponent<CursorManeger>().PointerCursor();

    }

    //Game over code.
    public void GameOver()
    {
        AllHudOff();
        gameOverText.gameObject.SetActive(true);
        restartButtonLose.gameObject.SetActive(true);
        returnButtonLose.gameObject.SetActive(true);
        isGameActive = false;
        Debug.Log("Player Died");
        cursorManeger.GetComponent<CursorManeger>().PointerCursor();

    }

    // Runs when the restart button is pressed.
    public void RestartGame()
    {
        StartCoroutine(restartSceneDelay());
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Runs when any of the starting buttons are pressed.
    public void StartGame()
    {
        isGameActive = true;
        Debug.Log("isGameActive is true");
        spawnManager = GameObject.Find("SpawnManager");
        wave = spawnManager.GetComponent<SpawnManager>().waveCount - 1;
        waveNumber.text = "WAVE -" + wave.ToString() + " OF 10";

        enemyNum = spawnManager.GetComponent<SpawnManager>().enemyCount + spawnManager.GetComponent<SpawnManager>().bossCounter;
        enemyCount.text = "ENEMY COUNT-" + enemyNum.ToString();


        player = GameObject.Find("Player");
        titleScreen.gameObject.SetActive(false);
        returnButtonMain.gameObject.SetActive(false);
        playerHud.gameObject.SetActive(true);

        inventoryUI.SetActive(true);
        ammoCount.SetActive(true);
        donutSilhouette.SetActive(true);


        cursorManeger.GetComponent<CursorManeger>().CrossHairCursor();
    }

    public void PlayDeathSound()
    {
        gameManagerAudio.Stop();
        gameManagerAudio.clip = deathSound;
        gameManagerAudio.Play();
    }

    public void PlayBossDeathSound()
    {
        gameManagerAudio.Stop();
        gameManagerAudio.clip = bossDeathSound;
        gameManagerAudio.Play();
    }

    public void AllHudOff()
    {
        playerHud.SetActive(false);
        inventoryUI.SetActive(false);
        ammoCount.SetActive(false);
        medKitCount.SetActive(false);
        donutSilhouette.SetActive(false);
        staffSilhouette.SetActive(false);

    }

    public void ALLHudOn()
    {
        playerHud.SetActive(true);
        inventoryUI.SetActive(true);
        ammoCount.SetActive(true);
        medKitCount.SetActive(true);
        donutSilhouette.SetActive(true);
        staffSilhouette.SetActive(true);

    }

    IEnumerator restartSceneDelay()
    {
        yield return new WaitForSeconds(RestartSceneDelayTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
