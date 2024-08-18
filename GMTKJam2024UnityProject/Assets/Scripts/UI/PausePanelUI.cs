using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanelUI : MonoBehaviour
{

    public static PausePanelUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        this.gameObject.SetActive(false);
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        this.gameObject.SetActive(true);
    }
    public void ResumeGame()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartCurrentLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BackToMainMenu()
    {
        //We suppose the main menu is always in the index 0 of the build order*
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void ExitApplication()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
