using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;
    public int damage = 1;
    public string ownerTag = "Player";

    private float timer = 0f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(int dmg, float spd, float life, string owner)
    {
        damage = dmg;
        speed = spd;
        lifeTime = life;
        ownerTag = owner;
        timer = lifeTime;
    }

    private void OnEnable()
    {
        timer = lifeTime;
    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        timer -= Time.deltaTime;
        if (timer <= 0f) ProjectilePool.Instance.Despawn(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(ownerTag)) return;

        // if hits enemy
        EnemyBase eb = other.GetComponent<EnemyBase>();
        if (eb != null)
        {
            eb.TakeDamage(damage);
            // spawn impact particle if assigned in Projectile (optional)
            ProjectilePool.Instance.Despawn(gameObject);
            return;
        }

        // if hits something else (e.g., wall) just despawn
        ProjectilePool.Instance.Despawn(gameObject);
    }
}