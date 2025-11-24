using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton -----------------------------------------
    public static GameManager Instance { get; private set; }

    // Dados de jogo -------------------------------------
    public int score = 0;
    public int lives = 3;
    public WeaponType currentWeapon = WeaponType.Rapid;

    // Eventos (HUD) -------------------------------------
    public IntEvent scoreEvent_SO;
    public IntEvent livesEvent_SO;
    public WeaponEvent weaponEvent_SO;

    // Save em memória -----------------------------------
    private SaveData save;

    
    private void Awake()
    {
        // Implementação do Singleton
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
        // Carrega o arquivo JSON
        save = SaveSystem.Load();

        // Inicializa score do jogo (ZERAR, não usar highScore)
        score = 0;
        scoreEvent_SO.Raise(score);

        // Vidas
        livesEvent_SO.Raise(lives);

        // Arma inicial
        SetWeapon(WeaponType.Rapid);
    }


    // ----------------------------------------------------
    // SCORE
    // ----------------------------------------------------
    public void AddScore(int amount)
    {
        score += amount;

        // HUD
        scoreEvent_SO.Raise(score);

        // Atualiza HighScore
        if (score > save.highScore)
            save.highScore = score;
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
        weaponEvent_SO.Raise(currentWeapon);
    }


    // ----------------------------------------------------
    // GAME OVER
    // ----------------------------------------------------
    private void OnGameOver()
    {
        Debug.Log("GAME OVER!");
        SaveGame();
        // futuramente: carregar tela GameOver
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
    }
}
