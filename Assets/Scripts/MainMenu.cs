using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    public Button PlayButton;
    public Button ControlButton;
    public Button GoBackButton;
    public Button Quit;
    public Button Tutorial;

    private void Start()
    {
        if (PlayButton != null && ControlButton != null && GoBackButton != null && Quit != null && Tutorial != null)
        {
            PlayButton.onClick.AddListener(LoadGame);
            ControlButton.onClick.AddListener(GoToControls);
            GoBackButton.onClick.AddListener(GoBack);
            Quit.onClick.AddListener(QuitGame); // Add this line to add event listener for Quit button
            Tutorial.onClick.AddListener(HowToPlay);
        }
    }

    public void LoadGame()
    {
        Time.timeScale = 1; // if the game starts without this and you load to another scene the timescale goes back to 0 so with this the level will always start with timescale at 1
        SceneManager.LoadScene(3);
        GameManager.Instance.DestroyEveryThing();
    }

    public void GoBack()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToControls()
    {
        SceneManager.LoadScene(1);
    }
    public void HowToPlay()
    {
        Time.timeScale = 1; // if the game starts without this and you load to another scene the timescale goes back to 0 so with this the level will always start with timescale at 1
        SceneManager.LoadScene(5);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Resume()
    {
        GameManager.Instance.TogglePause();
    }

    public void GoToMainMenu()
    {
        Resume(); // if this is not in the game will freeze when trying to load either the level or the tutorial.
        SceneManager.LoadScene(0);
        GameManager.Instance.DestroyEveryThing();
    }
}

