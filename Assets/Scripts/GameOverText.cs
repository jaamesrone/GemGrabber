using UnityEngine;
using TMPro;

public class GameOverText : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    private void Start()
    {
        float time = PlayerPrefs.GetFloat("Time Alive", 0f);
        int score = PlayerPrefs.GetInt("Score", 0);
        int lives = PlayerPrefs.GetInt("Lives", 3);

        timeText.text = "Time Alive : " + time.ToString("F2");
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
    }
}
