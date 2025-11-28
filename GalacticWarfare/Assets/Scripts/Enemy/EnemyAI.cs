using UnityEngine;

public class EnemyAI : EnemyBase
{
    [Header("Configurações Gerais")]
    public float chaseDistance = 6f;
    public float attackDistance = 3f;
    public int life = 3;

    private Transform player;

    protected override void Awake()
    {
        base.Awake();

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
    }

    private void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);
        anim.SetFloat("DistanceToPlayer", dist);

        // ⬇ Correção: Movimentação original mantida, mas mais suave
        if (dist < chaseDistance && dist > attackDistance)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            transform.position += dir * data.speed * Time.deltaTime;
        }

        if (dist <= attackDistance)
        {
            // Aqui você coloca ataque futuramente (atirar, colisão, etc)
        }
    }

    public override void TakeDamage(int dmg)
    {
        life -= dmg;

        if (life <= 0)
            Die();
    }

    protected override void Die()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.AddScore(10);

        Destroy(gameObject);
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            col.collider.GetComponent<PlayerHealth>().TakeDamage(1); // tira vida do player
            // NÃO DESTRÓI O INIMIGO!
        }
    }

}