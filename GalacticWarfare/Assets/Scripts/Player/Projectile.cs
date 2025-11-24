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
        // ignora colisão com quem atirou
        if (other.CompareTag(ownerTag)) return;

        // 1) Se for inimigo usando EnemyBase
        EnemyBase eb = other.GetComponent<EnemyBase>();
        if (eb != null)
        {
            eb.TakeDamage(damage);
            ProjectilePool.Instance.Despawn(gameObject);
            return;
        }

        // 2) Se for inimigo usando EnemyAI
        EnemyAI ea = other.GetComponent<EnemyAI>();
        if (ea != null)
        {
            ea.TakeDamage(damage);
            ProjectilePool.Instance.Despawn(gameObject);
            return;
        }

        // 3) Qualquer outra colisão (parede etc)
        ProjectilePool.Instance.Despawn(gameObject);
    }
}