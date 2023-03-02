using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


public class GameManager : Singleton<GameManager> //GameManager talks/inherit to singleton
{
    public int lives;
    public int score;
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI livesUI;

    // private text score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        UpdateLives();
    }

    public void UpdateScore()
    {
        scoreUI.text = "Score: " + score.ToString();
    }

    public void UpdateLives()
    {
        livesUI.text = "Lives: " + lives.ToString();
    }
}
