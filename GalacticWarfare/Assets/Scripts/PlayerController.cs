using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public WeaponData rapidData, rocketData, laserData;

    Rigidbody2D rb;
    Vector2 move;
    float fireTimer;

    WeaponData currentWeapon;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetWeapon(GameManager.Instance.currentWeapon);
    }

    void Update()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");

        HandleWeaponInput();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = move.normalized * speed;
    }

    void HandleWeaponInput()
    {
        fireTimer += Time.deltaTime;
        if (Input.GetButton("Fire1") && fireTimer >= currentWeapon.fireRate)
        {
            Shoot();
            fireTimer = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeWeapon(WeaponType.Rapid);
        if (Input.GetKeyDown(KeyCode.Alpha2)) ChangeWeapon(WeaponType.Rocket);
        if (Input.GetKeyDown(KeyCode.Alpha3)) ChangeWeapon(WeaponType.Laser);
    }

    void ChangeWeapon(WeaponType w)
    {
        SetWeapon(w);
        GameManager.Instance.SetWeapon(w);
    }

    void SetWeapon(WeaponType w)
    {
        switch (w)
        {
            case WeaponType.Rapid: currentWeapon = rapidData; break;
            case WeaponType.Rocket: currentWeapon = rocketData; break;
            case WeaponType.Laser: currentWeapon = laserData; break;
        }
    }

    void Shoot()
    {
        if (currentWeapon.projectilePrefab == null) return;
        var proj = ProjectilePool.Instance.Spawn(currentWeapon.projectilePrefab, transform.position + Vector3.right * 0.6f, Quaternion.identity);
        var p = proj.GetComponent<Projectile>();
        if (p != null) p.Initialize(currentWeapon.damage, Vector2.right);
    }
}