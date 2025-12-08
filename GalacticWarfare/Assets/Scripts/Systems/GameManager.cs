using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton -----------------------------------------
    public static GameManager Instance { get; private set; }

    // Dados de jogo -------------------------------------
    public int score = 0;
    public int lives = 5;
    public WeaponType currentWeapon;

    // Eventos (HUD) -------------------------------------
    public IntEvent scoreEvent_SO;
    public IntEvent livesEvent_SO;
    public WeaponEvent weaponEvent_SO;

    // Save em memória -----------------------------------
    private SaveData save;

    // Condição de vitória -------------------------------
    public int scoreToWin = 200;
    private bool gameWon = false;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        save = SaveSystem.Load();

        score = 0;
        scoreEvent_SO.Raise(score);

        livesEvent_SO.Raise(lives);

        weaponEvent_SO.Raise(currentWeapon);
    }


    // ----------------------------------------------------
    // SCORE
    // ----------------------------------------------------
    public void AddScore(int amount)
    {
        if (gameWon) return;

        score += amount;
        scoreEvent_SO.Raise(score);

        if (score > save.highScore)
            save.highScore = score;

        if (score >= scoreToWin)
        {
            OnVictory();
        }
    }


    // ----------------------------------------------------
    // VITÓRIA
    // ----------------------------------------------------
    private void OnVictory()
    {
        Debug.Log("🎉 VITÓRIA! Pontuação atingida.");

        gameWon = true;
        SaveGame();
        SceneManager.LoadScene("WinScene");
    }


    // ----------------------------------------------------
    // VIDAS
    // ----------------------------------------------------
    public void ChangeLives(int delta)
    {
        lives += delta;
        livesEvent_SO.Raise(lives);

        if (lives <= 0)
            OnGameOver();
    }


    // ----------------------------------------------------
    // ARMA
    // ----------------------------------------------------
    public void SetWeapon(WeaponType wt)
    {
        currentWeapon = wt;
        if (weaponEvent_SO != null)
            weaponEvent_SO.Raise(currentWeapon);
    }


    // ----------------------------------------------------
    // GAME OVER (PÚBLICO)
    // ----------------------------------------------------
    public void OnGameOver()
    {
        Debug.Log("GAME OVER!");
        SaveGame();
        SceneManager.LoadScene("GameOverScene");
    }


    // ----------------------------------------------------
    // SAVE
    // ----------------------------------------------------
    public void SaveGame()
    {
        save.lastScore = score;
        save.lastWeaponUnlocked = currentWeapon.ToString();

        SaveSystem.Save(save);

        Debug.Log("Jogo salvo com sucesso!");
    }


    // ----------------------------------------------------
    // LOAD MANUAL
    // ----------------------------------------------------
    public void LoadGame()
    {
        SaveData data = SaveSystem.Load();

        score = 0;
        lives = 3;
        currentWeapon = WeaponType.Rapid;

        scoreEvent_SO.Raise(score);
        livesEvent_SO.Raise(lives);
        weaponEvent_SO.Raise(currentWeapon);
    }


    // ----------------------------------------------------
    // Chamado pelo Player quando morre
    // ----------------------------------------------------
    public void OnPlayerDeath()
    {
        SaveGame();
        OnGameOver();  // AGORA FUNCIONA!
    }
}
