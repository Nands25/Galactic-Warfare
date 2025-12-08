using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinScreenController : MonoBehaviour
{
    public TMP_Text scoreText;

    private void Start()
    {
        // Mostra o score final do jogador
        int finalScore = GameManager.Instance.score;
        scoreText.text = "Final Score: " + finalScore;
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