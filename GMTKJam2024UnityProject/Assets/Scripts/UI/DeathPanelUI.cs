using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPanelUI : MonoBehaviour
{

    [SerializeField] public static DeathPanelUI Instance;

    [SerializeField] public TMP_Text explainationText;

    private void Awake()
    {
        Instance = this;
        this.gameObject.SetActive(false);
    }

    private readonly string timeOutText = "You were too slow, the beast grew too hungry and has decided to eat you.\n She will find something else to do her bidding";
    private readonly string fieldDeathText = "You were slain on the field, the beast will be sad... but mostly hungry.\r\n";


    public void ShowDeathPanel()
    {
        Time.timeScale = 0f;
        this.gameObject.SetActive(true);
    }

    public void HideDeathPanel()
    {
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
    }

    public void SetExplanationToTimeOut()
    {
        explainationText.text = timeOutText;
    }

    public void SetExplanationToFieldDeath()
    {
        explainationText.text = fieldDeathText;
    }

    public void RestartCurrentLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        //We suppose the main menu is always in the index 0 of the build order
        SceneManager.LoadScene(0);
    }

    public void ExitApplication()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
