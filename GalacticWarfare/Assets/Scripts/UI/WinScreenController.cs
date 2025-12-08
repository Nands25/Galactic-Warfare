using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinScreenController : MonoBehaviour
{
    public TMP_Text scoreText;

    private void Start()
    {
        // Carrega o último score salvo
        SaveData data = SaveSystem.Load();
        scoreText.text = "Final Score: " + data.lastScore;
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