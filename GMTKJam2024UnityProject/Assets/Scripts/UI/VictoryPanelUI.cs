using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryPanelUI : MonoBehaviour
{

    [SerializeField] private int currentLevel;
    [SerializeField] private Image star1;
    [SerializeField] private Image star2;
    [SerializeField] private Image star3;

    [SerializeField] private TMP_Text tripText;

    [SerializeField] private Sprite FilledStar;

    public static VictoryPanelUI Instance { get; private set; }

    [SerializeField] private Button nextLevelButton;

    private void Awake()
    {
        Instance = this;
        this.gameObject.SetActive(false);
    }

    public void Update()
    {
        // check in resources if scene with level + 1 exists
        
        Scene scene = SceneManager.GetSceneByName("Level" + (currentLevel + 1));

        if (scene.IsValid())
        {
            nextLevelButton.interactable = true;
        }
        else
        {
            nextLevelButton.interactable = false;
        }
    }

    public void RestartCurrentLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowVictoryPanel(int trip)
    {
        AudioPlayer.audioPlayer.PlayWinAudio();

        this.gameObject.SetActive(true);

        int score = LevelDataList.GetScore(currentLevel, trip);

        Image[] stars = { star1, star2, star3 };

        for (int i = 0; i < score; i++)
        {
            stars[i].sprite = FilledStar;
        }

        tripText.text = trip.ToString();

        int currentScore = Save.LoadScore(currentLevel);
        if (currentScore > trip)
            Save.SaveScore(currentLevel, trip);

        Time.timeScale = 0f;
    }

    public void GoNextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level" + (currentLevel + 1));
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
