using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI weaponText;

    public IntEvent scoreEvent_SO;
    public IntEvent livesEvent_SO;
    public WeaponEvent weaponEvent_SO;

    private void OnEnable()
    {
        Debug.Log("<color=yellow>HUD ATIVOU e registrou eventos!</color>");

        scoreEvent_SO.OnRaised += UpdateScoreHUD;
        livesEvent_SO.OnRaised += UpdateLivesHUD;
        weaponEvent_SO.OnRaised += UpdateWeaponHUD;
    }

    private void Start()
    {
        Debug.Log("<color=green>HUD START CHAMADO!</color>");
        weaponText.text = "WEAPON: (aguardando)";
    }

    private void OnDisable()
    {
        scoreEvent_SO.OnRaised -= UpdateScoreHUD;
        livesEvent_SO.OnRaised -= UpdateLivesHUD;
        weaponEvent_SO.OnRaised -= UpdateWeaponHUD;
    }

    void UpdateScoreHUD(int v)
    {
        Debug.Log("<color=cyan>[HUD] SCORE recebido:</color> " + v);
        scoreText.text = "SCORE: " + v;
    }

    void UpdateLivesHUD(int v)
    {
        Debug.Log("<color=red>[HUD] LIVES recebido:</color> " + v);
        livesText.text = "LIVES: " + v;
    }

    void UpdateWeaponHUD(WeaponType w)
    {
        Debug.Log("<color=lime>[HUD] WEAPON recebida:</color> " + w);
        weaponText.text = "WEAPON: " + w.ToString();
    }
}