using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
     GameObject gameOverPanel;

    [SerializeField]
    TextMeshProUGUI scoreText, bestScoreText;

    public static GameManager instance = null;

    void Awake()
    {
        Time.timeScale = 1f;
    }

    void Start()
    {
        if (instance == null)
            instance = this;
    }

    public void gameOver()
    {
        StartCoroutine(gameOverCoroutine());
    }

    IEnumerator gameOverCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        Time.timeScale = 0.01f;         //slow mo effect
        yield return new WaitForSecondsRealtime(0.5f);
        gameOverPanel.SetActive(true);
        scoreText.text = ScoreManager.instance.currentScore.ToString();
        bestScoreText.text = PlayerPrefs.GetInt("Best", 0).ToString();

        yield break;
    }
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
 