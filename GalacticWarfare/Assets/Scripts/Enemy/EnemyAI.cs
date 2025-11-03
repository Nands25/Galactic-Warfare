using UnityEngine;

public class EnemyAI : EnemyBase
{
    public float chaseDistance = 6f;
    public float attackDistance = 3f;
    protected Transform player;

    protected override void Awake()
    {
        base.Awake();
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    private void Update()
    {
        if (player == null) return;
        float dist = Vector3.Distance(transform.position, player.position);
        anim.SetFloat("DistanceToPlayer", dist);

        if (dist < chaseDistance && dist > attackDistance)
        {
            // move towards player
            Vector3 dir = (player.position - transform.position).normalized;
            transform.position += dir * data.speed * Time.deltaTime;
        }
        else if (dist <= attackDistance)
        {
            // attack behaviour (could shoot)
        }
    }
}