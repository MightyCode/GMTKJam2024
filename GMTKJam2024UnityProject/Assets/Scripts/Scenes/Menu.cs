using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] public static Menu Instance;

    [SerializeField] private int levelNumber;

    [SerializeField] private GameObject levelUISlot;

    [SerializeField] private Sprite FilledStar;

    private int MaxLevelPerLine = 3;

    void Awake()
    {
        Time.timeScale = 1f;
        this.gameObject.SetActive(true);

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        float screenSizeY = 1200;

        float startY = 200;
        float sizeY = 400;

        // Construct the UI for level selection based on the number of levels
        // Inside LevelUISlot, we have a button named LevelButton, three stars (Star1, Star2, Star3) and a trip value
        for (int i = 0; i < levelNumber; i++)
        {
            GameObject levelUI = Instantiate(levelUISlot, this.transform);

            Button button = levelUI.GetComponentInChildren<Button>();
            button.name = "Level" + (i + 1);
            button.onClick.AddListener(() =>
            {
                OpenLevel(button.name);
            });

            TMP_Text levelText = button.GetComponentInChildren<TMP_Text>();
            levelText.text = "Level " + (i + 1);

            GameObject tripValue = levelUI.transform.Find("TripValue").gameObject;

            int score = Save.LoadScore(i + 1);
            if (score == 0)
            {
                if (i != 0)
                {
                    int previousScore = Save.LoadScore(i);

                    if (previousScore == 0)
                    {
                        button.interactable = false;
                    }
                }

                GameObject trip = levelUI.transform.Find("Trip").gameObject;
                trip.SetActive(false);
                
                tripValue.SetActive(false);
            } else
            {
                tripValue.GetComponent<TMP_Text>().text = score.ToString();

                // Get image child stars
                Image[] stars =
                {
                    levelUI.transform.Find("Star1").GetComponent<Image>(),
                    levelUI.transform.Find("Star2").GetComponent<Image>(),
                    levelUI.transform.Find("Star3").GetComponent<Image>()
                };

                int starScore = LevelDataList.GetScore(i + 1, score);

                for (int j = 0; j < starScore; j++)
                {
                    stars[j].sprite = FilledStar;
                }
            }

            int indexRow = i / MaxLevelPerLine;
            int rowDiminish = Mathf.FloorToInt((levelNumber - 1) / MaxLevelPerLine);

            int levelInThisLine = MaxLevelPerLine;
            if (indexRow == rowDiminish)
            {
                levelInThisLine = levelNumber % MaxLevelPerLine;

                if (levelInThisLine == 0)
                {
                    levelInThisLine = MaxLevelPerLine;
                }
            }

            float positionX = 0;

            if (levelInThisLine == 2)
            {
                if (i % MaxLevelPerLine == 0)
                {
                    positionX = -screenSizeY / 3;
                }
                else
                {
                    positionX = screenSizeY / 3;
                }
            } else if (levelInThisLine == 3)
            {
                if (i % MaxLevelPerLine == 0)
                {
                    positionX = -screenSizeY / 2;
                }
                else if (i % MaxLevelPerLine == 2)
                {
                    positionX = screenSizeY / 2;
                }
            }

            levelUI.transform.localPosition = new Vector3(
                positionX, 
                startY - (int)(i / MaxLevelPerLine) * sizeY, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OpenLevel(string level)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(level);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
