using UnityEngine;

[CreateAssetMenu(menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int maxHp = 3;
    public float speed = 2f;
    public int scoreValue = 10;
}