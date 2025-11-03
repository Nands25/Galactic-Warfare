using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyBase : MonoBehaviour
{
    public EnemyData data;
    public int currentHp;
    protected Animator anim;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        if (data != null) currentHp = data.maxHp;
    }

    public virtual void TakeDamage(int amount)
    {
        currentHp -= amount;
        if (anim != null) anim.SetTrigger("Hit");
        if (currentHp <= 0) Die();
    }

    protected virtual void Die()
    {
        anim?.SetTrigger("Die");
        // award score
        if (data != null) GameManager.Instance?.AddScore(data.scoreValue);
        // play explosion particle (optional)
        // disable collider to avoid further hits
        Collider2D c = GetComponent<Collider2D>();
        if (c != null) c.enabled = false;
        // if you want to destroy after animation, call Destroy in animation event or coroutine
        Destroy(gameObject, 1.0f); // fallback
    }
}