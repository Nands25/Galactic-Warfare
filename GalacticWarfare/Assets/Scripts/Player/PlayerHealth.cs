using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxLife = 5;
    public int currentLife;

    public IntEvent hudLifeEvent; // EVENTO HUD

    void Start()
    {
        currentLife = maxLife;
        hudLifeEvent.Raise(currentLife);
    }

    public void TakeDamage(int dmg)
    {
        currentLife -= dmg;
        hudLifeEvent.Raise(currentLife);

        if (currentLife <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("PLAYER MORREU");
        GameManager.Instance.OnPlayerDeath();
    }
}