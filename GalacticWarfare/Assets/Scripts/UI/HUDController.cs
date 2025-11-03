using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public IntEvent scoreEvent_SO;
    public IntEvent livesEvent_SO;
    public WeaponEvent weaponEvent_SO;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI weaponText;

    private void OnEnable()
    {
        if (scoreEvent_SO != null) scoreEvent_SO.OnRaised += OnScoreChanged;
        if (livesEvent_SO != null) livesEvent_SO.OnRaised += OnLivesChanged;
        if (weaponEvent_SO != null) weaponEvent_SO.OnRaised += OnWeaponChanged;
    }

    private void OnDisable()
    {
        if (scoreEvent_SO != null) scoreEvent_SO.OnRaised -= OnScoreChanged;
        if (livesEvent_SO != null) livesEvent_SO.OnRaised -= OnLivesChanged;
        if (weaponEvent_SO != null) weaponEvent_SO.OnRaised -= OnWeaponChanged;
    }

    private void OnScoreChanged(int s)
    {
        if (scoreText != null) scoreText.text = $"SCORE: {s}";
    }

    private void OnLivesChanged(int l)
    {
        if (livesText != null) livesText.text = $"LIVES: {l}";
    }

    private void OnWeaponChanged(WeaponType wt)
    {
        if (weaponText != null) weaponText.text = $"WEAPON: {wt}";
    }
}