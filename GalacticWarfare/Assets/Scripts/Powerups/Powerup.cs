using UnityEngine;

public class Powerup : MonoBehaviour
{
    public PowerupData data;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        ApplyTo(other.gameObject);
        Destroy(gameObject);
    }

    private void ApplyTo(GameObject player)
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        if (pc == null) return;

        switch (data.type)
        {
            case PowerupType.Shield:
                // implement shield logic (add shield to player)
                Debug.Log("Shield picked");
                break;
            case PowerupType.Ammo:
                // refill ammo for the currently selected weapon
                WeaponData wd = pc.GetCurrentWeaponData();
                if (wd != null && wd.maxAmmo > 0)
                    wd.currentAmmo = Mathf.Min(wd.maxAmmo, wd.currentAmmo + data.amount);
                Debug.Log("Ammo picked");
                break;
            case PowerupType.Super:
                // super shot (invincibility or screen clear)
                Debug.Log("Super picked");
                break;
        }
    }
}