using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private int damage = 10;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private float maxTravelDistance = 10f;

    private Vector2 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        float traveledDistance = Vector2.Distance(startPosition, transform.position);

        if (traveledDistance >= maxTravelDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            BaseHealth enemyHealth = collision.GetComponent<BaseHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            HandleCollision();
        }
        else if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
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
        // Hiển thị hiệu ứng nổ (nếu có)
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Hủy viên đạn
        Destroy(gameObject);
    }

}
