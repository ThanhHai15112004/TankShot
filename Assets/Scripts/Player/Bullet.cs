using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    private int damage; // Sát thương của viên đạn
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private float maxTravelDistance = 10f;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip explosionSound; // Âm thanh khi nổ
    private AudioSource audioSource;

    private Vector2 startPosition;

    private void Start()
    {
        startPosition = transform.position;

        // Thêm AudioSource nếu chưa có
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        float traveledDistance = Vector2.Distance(startPosition, transform.position);

        if (traveledDistance >= maxTravelDistance)
        {
            Destroy(gameObject);
        }
    }

    public void SetDamage(int bulletDamage)
    {
        damage = bulletDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Player"))
        {
            BaseHealth health = collision.GetComponent<BaseHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
            HandleCollision();
        }
        else if (collision.CompareTag("Obstacle"))
        {
            HandleCollision();
        }
    }

    private void HandleCollision()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        PlayExplosionSound();
        Destroy(gameObject);
    }

    private void PlayExplosionSound()
    {
        if (audioSource != null && explosionSound != null)
        {
            audioSource.PlayOneShot(explosionSound);
        }
    }
}

