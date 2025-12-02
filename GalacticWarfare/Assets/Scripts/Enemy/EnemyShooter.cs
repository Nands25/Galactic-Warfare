using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [Header("Configuração de tiro")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;     // tempo entre tiros
    public float shootDistance = 5f; // distância mínima para começar a atirar
    public float bulletSpeed = 7f;

    private Transform player;
    private float timer;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        // Só atira se o player estiver dentro da distância de tiro
        if (dist <= shootDistance)
        {
            timer += Time.deltaTime;

            if (timer >= fireRate)
            {
                Shoot();
                timer = 0f;
            }
        }
    }

    void Shoot()
    {
        Vector3 dir = (player.position - transform.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = dir * bulletSpeed;
    }
}