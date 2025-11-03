using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Player state")]
    public int lives = 3;
    public int score = 0;

    [Header("Events (ScriptableObjects)")]
    public IntEvent scoreEvent_SO;
    public IntEvent livesEvent_SO;
    public WeaponEvent weaponEvent_SO;

    [Header("Save")]
    public string saveFileName = "save_game.json";

    public WeaponType currentWeapon = WeaponType.Rapid;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // raise initial values
        scoreEvent_SO?.Raise(score);
        livesEvent_SO?.Raise(lives);
        weaponEvent_SO?.Raise(currentWeapon);
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreEvent_SO?.Raise(score);
    }

    public void ChangeLives(int delta)
    {
        lives += delta;
        if (lives < 0) lives = 0;
        livesEvent_SO?.Raise(lives);
        if (lives == 0) OnGameOver();
    }

    public void SetWeapon(WeaponType wt)
    {
        currentWeapon = wt;
        weaponEvent_SO?.Raise(currentWeapon);
    }

    private void OnGameOver()
    {
        // handle game over: for now, log
        Debug.Log("Game Over");
    }

    // Save / Load helpers -> wired to SaveSystem
    public void SaveGame()
    {
        SaveData sd = new SaveData { highScore = score, lastScore = score };
        SaveSystem.Save(sd, saveFileName);
    }

    public void LoadGame()
    {
        SaveData sd = SaveSystem.Load(saveFileName);
        if (sd != null)
        {
            score = sd.highScore;
            scoreEvent_SO?.Raise(score);
        }
    }
}