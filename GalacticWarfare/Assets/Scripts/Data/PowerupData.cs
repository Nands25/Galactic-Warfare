using UnityEngine;

public enum PowerupType { Shield, Ammo, Super }

[CreateAssetMenu(menuName = "Data/PowerupData")]
public class PowerupData : ScriptableObject
{
    public PowerupType type;
    public int amount = 1;
    public Sprite icon;
}