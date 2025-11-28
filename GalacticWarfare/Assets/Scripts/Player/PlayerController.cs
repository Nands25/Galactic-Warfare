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
    private float currentEnergy = 100f; // for laser weapon

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // se esquecer de arrastar no inspetor, cria automático
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

    // -------------------------------
    // MOVIMENTO
    // -------------------------------
    private void HandleMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector2 move = new Vector2(h, v).normalized;
        rb.linearVelocity = move * speed;
    }

    // -------------------------------
    // TIRO
    // -------------------------------
    private void HandleFire()
    {
        fireTimer -= Time.deltaTime;

        WeaponData wd = GetCurrentWeaponData();
        if (wd == null) return;

        // ATIRAR COM ESPAÇO
        if (Input.GetKey(KeyCode.Space) && fireTimer <= 0f)
        {
            // munição (se usar)
            if (wd.maxAmmo > 0)
            {
                if (wd.currentAmmo <= 0) return;
                wd.currentAmmo--;
            }

            // laser usa energia
            if (currentWeapon == WeaponType.Laser)
            {
                if (currentEnergy <= 0) return;
                currentEnergy -= 1f;
            }

            SpawnProjectile(wd);
            fireTimer = wd.fireRate;
        }

        // recarga de energia do laser
        currentEnergy = Mathf.Min(currentEnergy + 10f * Time.deltaTime, 100f);
    }

    // -------------------------------
    // TROCA DE ARMA (1,2,3)
    // -------------------------------
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

    // -------------------------------
    // SPAWN DO PROJÉTIL
    // -------------------------------
    private void SpawnProjectile(WeaponData wd)
    {
        if (wd.projectilePrefab == null)
        {
            Debug.LogError("Arma sem prefab de projétil!");
            return;
        }

        GameObject proj = ProjectilePool.Instance.Spawn(
            wd.projectilePrefab,
            projectileSpawn.position,
            projectileSpawn.rotation
        );

        Projectile p = proj.GetComponent<Projectile>();
        if (p != null)
        {
            p.Initialize(wd.damage, wd.projectileSpeed, wd.lifeTime, gameObject.tag);
        }
    }

    public void ChangeWeapon(WeaponType wt)
    {
        currentWeapon = wt;
        Debug.Log("Player trocou para -> " + wt);

        // 🔥 Atualiza HUD e GameManager
        GameManager.Instance.SetWeapon(wt);
    }
}
