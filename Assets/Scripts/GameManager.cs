using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static bool gameOver, levelCompleted, mute = false, isGameStarted;
    public static int currentLevelIndex, numberOfPassedRings, score;

    public GameObject gameOverPanel, levelCompletedPanel, gamePlayPanel, startMenuPanel;
    public TextMeshProUGUI currentLevelText, nextLevelText, scoreText, highScoreText;
    public Slider gameProgressSlider;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = levelCompleted = false;
        Time.timeScale = 1;
        numberOfPassedRings = 0;
        highScoreText.text = "Best Score\n" + PlayerPrefs.GetInt("HighScore", 0);
        isGameStarted = false;
    }

    private void Awake()
    {
        currentLevelIndex = PlayerPrefs.GetInt("Current Level Index", 1);
    }

    // Update is called once per frame
    void Update()
    {
        // Update UI
        currentLevelText.text = currentLevelIndex.ToString();
        nextLevelText.text = (currentLevelIndex + 1).ToString();

        int progress = numberOfPassedRings * 100 / FindObjectOfType<HelixManager>().numberOfRings;
        gameProgressSlider.value = progress;

        scoreText.text = score.ToString();

        // Start Level
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && !isGameStarted)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return;

            isGameStarted = true;
            gamePlayPanel.SetActive(true);
            startMenuPanel.SetActive(false);

        }

        // Game Over
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);

            if (Input.GetButtonDown("Fire1"))
            {
                if (score > PlayerPrefs.GetInt("HighScore", 0))
                {
                    PlayerPrefs.SetInt("HighScore", score);
                }
                score = 0;
                SceneManager.LoadScene("Level");
            }
        }

        if (levelCompleted)
        {
            levelCompletedPanel.SetActive(true);

            if (Input.GetButtonDown("Fire1"))
            {
                PlayerPrefs.SetInt("Current Level Index", currentLevelIndex + 1);
                SceneManager.LoadScene("Level");
            }
        }
    }
}
