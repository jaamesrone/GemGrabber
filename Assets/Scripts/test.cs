using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Test: MonoBehaviour
{
    public Button playButton;
    public Button ControlButton;
    public Button GoBackButton;
    public Button Quit;

    private void Start()
    {
        if (playButton != null && ControlButton != null && GoBackButton != null && Quit != null)
        {
            playButton.onClick.AddListener(LoadGame);
            ControlButton.onClick.AddListener(GoToControls);
            GoBackButton.onClick.AddListener(GoBack);
            Quit.onClick.AddListener(QuitGame); // Add this line to add event listener for Quit button
        }
    }

    public void LoadGame()
    {
        GameManager.Instance.DestroyEveryThing();
        SceneManager.LoadScene(3);
    }

    public void GoBack()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToControls()
    {
        SceneManager.LoadScene(1);
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
        SceneManager.LoadScene(0);
        //GameManager.Instance.DestroyEveryThing();
    }
}
