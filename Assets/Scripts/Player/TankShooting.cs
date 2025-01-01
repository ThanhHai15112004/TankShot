using System.Collections;
using UnityEngine;

public class TankShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private Transform turretBarrel;  // Vị trí đầu nòng súng
    [SerializeField] private GameObject bulletPrefab; // Prefab của viên đạn
    [SerializeField] private float bulletSpeed = 20f; // Tốc độ viên đạn
    [SerializeField] private float reloadTime = 0.5f; // Thời gian nạp đạn

    [Header("Effects")]
    [SerializeField] private GameObject muzzleFlashPrefab; // Hiệu ứng bắn (tia lửa)
    [SerializeField] private AudioClip shootSound; // Âm thanh bắn

    private bool canShoot = true;
    private AudioSource audioSource;
    private Collider2D tankCollider; // Bộ va chạm của xe tăng

    public bool IsPlayerControlled = false; // Cờ để phân biệt Player và Enemy

    private void Awake()
    {
        // Lấy AudioSource nếu có
        audioSource = GetComponent<AudioSource>();

        // Lấy Collider của xe tăng
        tankCollider = GetComponent<Collider2D>();
    }

    public void Shoot()
    {
        if (!canShoot) return;

        // Nếu là Player, chỉ bắn khi có lệnh
        if (IsPlayerControlled && !Input.GetMouseButtonDown(0)) return;

        SpawnAndShootBullet();
        PlayShootEffects();
        StartCoroutine(Reload());
    }

    private void SpawnAndShootBullet()
    {
        if (!turretBarrel || !bulletPrefab) return;

        // Tạo viên đạn mới
        GameObject bullet = Instantiate(bulletPrefab, turretBarrel.position, turretBarrel.rotation);

        // Đặt vận tốc cho đạn
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            bulletRb.linearVelocity = turretBarrel.up * bulletSpeed;
        }

        // Bỏ qua va chạm giữa đạn và xe tăng
        Collider2D bulletCollider = bullet.GetComponent<Collider2D>();
        if (bulletCollider != null && tankCollider != null)
        {
            Physics2D.IgnoreCollision(bulletCollider, tankCollider);
        }

        // Hủy đạn sau 5 giây để tránh "rác" trong game
        Destroy(bullet, 5f);
    }

    private void PlayShootEffects()
    {
        // Tạo hiệu ứng tia lửa
        if (muzzleFlashPrefab != null)
        {
            GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, turretBarrel.position, turretBarrel.rotation);
            Destroy(muzzleFlash, 0.5f);
        }

        // Phát âm thanh bắn
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    private IEnumerator Reload()
    {
        canShoot = false;
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
    }
}