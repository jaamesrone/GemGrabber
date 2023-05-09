using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


public class GameManager : Singleton<GameManager> //GameManager talks/inherit to singleton
{
    private float timer = 0f;
    public bool isPaused = false;
    public bool gameEnded = false;
    public int lives;
    public int score;
    public int maxHealth = 100;
    public int currentHealth;
    public TextMeshProUGUI powerEffectText;
    public TextMeshProUGUI healthSystem;
    public TextMeshProUGUI GameCountDown;
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI livesUI;
    public TextMeshProUGUI timerUI;
    public TextMeshProUGUI healthUI;
    public Transform SpawnPoint;
    public GameObject Player;
    public GameObject canvas;
    public GameObject PlayerPrefab;
    private PlayerController playerScript;


    public override void Awake()
    {
        base.Awake();
        Player = Instantiate(PlayerPrefab, SpawnPoint.position, SpawnPoint.rotation);

    }
    // private text score;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = Player.transform.GetComponent<PlayerController>();
        DontDestroyOnLoad(GameObject.Find("Canvas"));
        DontDestroyOnLoad(Player); 
        DontDestroyOnLoad(GameObject.Find("SpawnPoint"));
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthSystem();
        UpdatePowerEffects();
        UpdateGameCountDown();
        UpdateTime();
        UpdateScore();
        UpdateLives();
        PauseGame();
    }

    public void DestroyEveryThing()
    {
        Destroy(Player);
        Destroy(GameObject.Find("Canvas"));
        Destroy(gameObject);
    }


    public void UpdateScore()
    {
        scoreUI.text = "Score: " + score.ToString();
    }

    public void UpdateLives()
    {
        livesUI.text = "Lives: " + lives.ToString();
    }

    public void UpdateGameCountDown()
    {
        float countDown = playerScript.countDownTimer;

        GameCountDown.text = "Game Will Start In: " + countDown.ToString();

        if (countDown <= 0)
        {
            GameCountDown.gameObject.SetActive(false);
        }
    }

    public void UpdateHealthSystem()
    {
        healthSystem.text = "Health: " + currentHealth.ToString();

        if (currentHealth <= 0)
        {
            GameCountDown.gameObject.SetActive(false);
        }
    }

    public void UpdatePowerEffects()
    {
        if (playerScript.boosting)
        {
            float countDown = 3 - playerScript.boostTimer;
            powerEffectText.text = "Speed Boost: " + countDown.ToString("F1") + "s";
            powerEffectText.gameObject.SetActive(true);
        }
        else if (playerScript.decreaseBoosting)
        {
            float countDown = 5 - playerScript.decreaseTime;
            powerEffectText.text = "Speed Loss: " + countDown.ToString("F1") + "s";
            powerEffectText.gameObject.SetActive(true);
        }
        else if (playerScript.gravityOn)
        {
            float countDown = 10 - playerScript.gravityTimer;
            powerEffectText.text = "Gravity Off: " + countDown.ToString("F1") + "s";
            powerEffectText.gameObject.SetActive(true);
        }
        else
        {
            powerEffectText.gameObject.SetActive(false);
        }
    }


    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void UpdateTime()
    {
        timer += Time.deltaTime;
        timerUI.text = "Time Alive: " + timer.ToString("F2") + "s";
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
        }
        else
        {
            Time.timeScale = 1;
            SceneManager.UnloadSceneAsync("PauseMenu");
        }

        Debug.Log("Time.timeScale: " + Time.timeScale);
    }

    public void Respawn()
    {
        lives--;
        UpdateLives();
        Player.transform.position = SpawnPoint.localPosition;
        ResetPowerUps();
        if (lives <= 0)
        {
            SaveGame();
            SceneManager.LoadScene(4);
            lives = 3;
            score = 0;
            DestroyEveryThing();
        }
    }
    public void SaveGame()
    {
        PlayerPrefs.SetFloat("Time Alive", timer);
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Lives", lives);
        PlayerPrefs.Save();
    }

    private void ResetPowerUps()
    {
        playerScript.decreaseBoosting = false;
        playerScript.boosting = false;
        playerScript.gravityOn = false;
        playerScript.speed = 10;
        playerScript.rb.useGravity = true;
    }
}
