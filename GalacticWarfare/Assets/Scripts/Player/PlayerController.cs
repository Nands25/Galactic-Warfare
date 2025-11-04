using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;

    [Header("Weapons (ScriptableObjects)")]
    public WeaponData rapidWeapon;
    public WeaponData rocketWeapon;
    public WeaponData laserWeapon;

    [Header("Runtime")]
    public WeaponType currentWeapon = WeaponType.Rapid;
    public Transform projectileSpawn;

    private Rigidbody2D rb;
    private float fireTimer = 0f;
    private float currentEnergy = 100f; // for laser

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (projectileSpawn == null)
        {
            GameObject ps = new GameObject("ProjectileSpawn");
            ps.transform.SetParent(transform);
            ps.transform.localPosition = Vector3.right * 0.6f;
            projectileSpawn = ps.transform;
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleFire();
        HandleWeaponChange();
    }

    private void HandleMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector2 move = new Vector2(h, v).normalized;
        rb.linearVelocity = move * speed;
    }

    private void HandleFire()
    {
        fireTimer -= Time.deltaTime;
        WeaponData wd = GetCurrentWeaponData();
        if (wd == null) return;

        if (Input.GetButton("Fire1") && fireTimer <= 0f)
        {
            // check ammo
            if (wd.maxAmmo > 0)
            {
                if (wd.currentAmmo <= 0) return;
                wd.currentAmmo--;
            }

            // laser uses energy
            if (currentWeapon == WeaponType.Laser)
            {
                if (currentEnergy <= 0) return;
                currentEnergy -= 1f; // or more per shot
            }

            SpawnProjectile(wd);
            fireTimer = wd.fireRate;
        }

        // recharge energy slowly
        currentEnergy = Mathf.Min(currentEnergy + 10f * Time.deltaTime, 100f);
    }

    private void HandleWeaponChange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeWeapon(WeaponType.Rapid);
        if (Input.GetKeyDown(KeyCode.Alpha2)) ChangeWeapon(WeaponType.Rocket);
        if (Input.GetKeyDown(KeyCode.Alpha3)) ChangeWeapon(WeaponType.Laser);
    }

    public WeaponData GetCurrentWeaponData()
    {
        return currentWeapon switch
        {
            WeaponType.Rapid => rapidWeapon,
            WeaponType.Rocket => rocketWeapon,
            WeaponType.Laser => laserWeapon,
            _ => rapidWeapon
        };
    }

    private void SpawnProjectile(WeaponData wd)
    {
        if (wd.projectilePrefab == null) return;
        GameObject proj = ProjectilePool.Instance.Spawn(wd.projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
        Projectile p = proj.GetComponent<Projectile>();
        if (p != null) p.Initialize(wd.damage, wd.projectileSpeed, wd.lifeTime, gameObject.tag);
    }

    public void ChangeWeapon(WeaponType wt)
    {
        currentWeapon = wt;
        GameManager.Instance?.SetWeapon(wt);
    }
}
