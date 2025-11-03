using UnityEngine;

public enum WeaponType { Rapid, Rocket, Laser }

[CreateAssetMenu(menuName = "Data/WeaponData")]
public class WeaponData : ScriptableObject
{
    public WeaponType weaponType;
    public float fireRate = 0.2f;
    public int damage = 1;
    public int maxAmmo = -1; // -1 infinite
    [HideInInspector] public int currentAmmo = -1;
    public float projectileSpeed = 12f;
    public float lifeTime = 3f;
    public GameObject projectilePrefab;

    private void OnEnable()
    {
        // initialize ammo
        if (maxAmmo > 0) currentAmmo = maxAmmo;
        else currentAmmo = -1;
    }
}