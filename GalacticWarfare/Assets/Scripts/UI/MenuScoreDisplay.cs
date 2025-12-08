using UnityEngine;
using TMPro;

public class MenuScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI lastScoreText;
    public TextMeshProUGUI highScoreText;

    private void Start()
    {
        SaveData data = SaveSystem.Load();

        if (lastScoreText != null)
            lastScoreText.text = "Last Score: " + data.lastScore;

        if (highScoreText != null)
            highScoreText.text = "Best Score: " + data.highScore;
    }
}