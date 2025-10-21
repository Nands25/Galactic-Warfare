using UnityEngine;

public enum WeaponType { Rapid, Rocket, Laser }

[CreateAssetMenu(menuName = "Data/WeaponData")]
public class WeaponData : ScriptableObject
{
    public WeaponType weaponType;
    public float fireRate = 0.2f;
    public int damage = 1;
    public int maxAmmo = 50; // for rocket
    public float energy = 100f; // for laser 0-100
    public GameObject projectilePrefab;
}