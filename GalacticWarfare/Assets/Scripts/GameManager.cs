using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int score;
    public int lives = 3;

    public WeaponType currentWeapon = WeaponType.Rapid;

    public event Action<int> OnScoreChanged;
    public event Action<int> OnLivesChanged;
    public event Action<WeaponType> OnWeaponChanged;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddScore(int amount)
    {
        score += amount;
        OnScoreChanged?.Invoke(score);
    }

    public void ChangeLives(int delta)
    {
        lives += delta;
        OnLivesChanged?.Invoke(lives);
    }

    public void SetWeapon(WeaponType wt)
    {
        currentWeapon = wt;
        OnWeaponChanged?.Invoke(wt);
    }
}