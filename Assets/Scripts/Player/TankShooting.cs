using System.Collections;
using UnityEngine;

public abstract class TankShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] protected Transform turretBarrel;
    [SerializeField] protected Transform effectPoint;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected float bulletSpeed = 20f;
    [SerializeField] protected float reloadTime = 0.5f;
    [SerializeField] protected int damage = 10;

    [Header("Effects")]
    [SerializeField] protected GameObject muzzleFlashPrefab;
    [SerializeField] protected AudioClip shootSound;

    protected bool canShoot = true;
    protected AudioSource audioSource;
    protected Collider2D tankCollider;
    [SerializeField] protected AimTurret aimTurret;
    private float reloadTimer;

    public bool CanShoot()
    {
        return canShoot;
    }

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        tankCollider = GetComponent<Collider2D>();
        aimTurret = GetComponentInChildren<AimTurret>();
    }

    public virtual void Shoot()
    {
        if (!canShoot) return;
        SpawnAndShootBullet();
        PlayShootEffects();
        StartCoroutine(Reload());
    }

    protected virtual void SpawnAndShootBullet()
    {
        if (turretBarrel == null || bulletPrefab == null) return;

        GameObject bullet = Instantiate(bulletPrefab, turretBarrel.position, turretBarrel.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            bulletRb.linearVelocity = turretBarrel.up * bulletSpeed;
        }

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDamage(damage);
        }

        Collider2D bulletCollider = bullet.GetComponent<Collider2D>();
        if (bulletCollider != null && tankCollider != null)
        {
            Physics2D.IgnoreCollision(bulletCollider, tankCollider);
        }

        Destroy(bullet, 5f);
    }

    protected virtual void PlayShootEffects()
    {
        if (muzzleFlashPrefab != null && effectPoint != null)
        {
            GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, effectPoint.position, effectPoint.rotation);
            Destroy(muzzleFlash, 0.5f);
        }

        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    protected IEnumerator Reload()
    {
        canShoot = false;
        reloadTimer = 0f; // Đặt lại timer

        while (reloadTimer < reloadTime)
        {
            reloadTimer += Time.deltaTime; // Tăng timer
            yield return null;
        }

        canShoot = true;
        reloadTimer = reloadTime; // Đặt đầy sau khi hoàn tất
    }

    public float GetReloadProgress()
    {
        return Mathf.Clamp01(reloadTimer / reloadTime); // Tiến trình nạp đạn (0 - 1)
    }
}