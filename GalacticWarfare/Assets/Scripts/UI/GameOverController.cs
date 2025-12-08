using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public TMP_Text scoreText;

    void Start()
    {
        int finalScore = GameManager.Instance.score;
        scoreText.text = "SCORE FINAL: " + finalScore;
    }

    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}