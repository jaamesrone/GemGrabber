using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameSceneTransitions : MonoBehaviour
{
    public Button playButton;
    public Button ControlButton;
    public Button GoBackButton;
    public Button Quit;

    private void Start()
    {
        playButton.onClick.AddListener(LoadGame);
        ControlButton.onClick.AddListener(GoToControls);
        GoBackButton.onClick.AddListener(GoBack);
    }


    public void LoadGame()
    {

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

}
