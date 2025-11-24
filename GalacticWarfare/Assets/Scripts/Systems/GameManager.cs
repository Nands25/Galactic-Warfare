using UnityEngine;

public class GameManager : MonoBehaviour
{
    // -------------------------------
    // Campos principais do jogo
    // -------------------------------

    public int score = 0;
    public int lives = 3;

    public WeaponType currentWeapon;

    // Eventos para HUD
    public IntEvent scoreEvent_SO;
    public IntEvent livesEvent_SO;
    public WeaponEvent weaponEvent_SO;

    // SaveData em memória
    private SaveData save;


    // -------------------------------
    // Inicialização
    // -------------------------------
    void Start()
    {
        // Carrega o save ao iniciar o jogo
        save = SaveSystem.Load();

        // Atualiza score e HUD
        score = save.highScore;
        scoreEvent_SO.Raise(score);

        // Atualiza HUD de vidas
        livesEvent_SO.Raise(lives);

        // Atualiza arma atual (padrão)
        SetWeapon(WeaponType.Rapid);
    }


    // -------------------------------
    // Score
    // -------------------------------
    public void AddScore(int amount)
    {
        score += amount;

        // Atualiza HUD
        scoreEvent_SO.Raise(score);

        // Atualiza high score no save
        if (score > save.highScore)
            save.highScore = score;
    }


    // -------------------------------
    // Vidas
    // -------------------------------
    public void ChangeLives(int delta)
    {
        lives += delta;
        livesEvent_SO.Raise(lives);

        if (lives <= 0)
            OnGameOver();
    }


    // -------------------------------
    // Armas
    // -------------------------------
    public void SetWeapon(WeaponType wt)
    {
        currentWeapon = wt;
        weaponEvent_SO.Raise(currentWeapon);
    }


    // -------------------------------
    // Game Over
    // -------------------------------
    private void OnGameOver()
    {
        Debug.Log("GAME OVER");

        SaveGame();

        // Aqui você pode carregar uma tela de GameOver depois
        // SceneManager.LoadScene("GameOver");
    }


    // -------------------------------
    // SAVE
    // -------------------------------
    public void SaveGame()
    {
        save.lastScore = score;
        save.lastWeaponUnlocked = currentWeapon.ToString();

        SaveSystem.Save(save);

        Debug.Log("Jogo salvo.");
    }


    // -------------------------------
    // LOAD
    // -------------------------------
    public void LoadGame()
    {
        SaveData sd = SaveSystem.Load();

        if (sd != null)
        {
            score = sd.highScore;
            scoreEvent_SO.Raise(score);
        }
    }


    // -------------------------------
    // Quando o jogador morre
    // -------------------------------
    public void OnPlayerDeath()
    {
        SaveGame();
    }
}
