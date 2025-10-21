using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 8f;
    public int damage;
    public Vector2 direction = Vector2.right;
    public float lifeTime = 3f;

    Rigidbody2D rb;

    void Awake() { rb = GetComponent<Rigidbody2D>(); }

    public void Initialize(int dmg, Vector2 dir)
    {
        damage = dmg;
        direction = dir.normalized;
        StartCoroutine(AutoDespawn());
    }

    void FixedUpdate()
    {
        rb.linearVelocity = direction * speed;
    }

    IEnumerator AutoDespawn()
    {
        yield return new WaitForSeconds(lifeTime);
        ProjectilePool.Instance.Despawn(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        var enemy = col.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            // spawn impact particle (hooked in enemy or pool)
            ProjectilePool.Instance.Despawn(gameObject);
        }
    }
}