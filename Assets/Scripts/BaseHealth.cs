using UnityEngine;

public abstract class BaseHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100; // Máu tối đa
    private int currentHealth;

    private void Start()
    {
        InitializeHealth();
    }

    private void InitializeHealth()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            return;
        }

        currentHealth = Mathf.Max(currentHealth - damage, 0);

        // Kiểm tra nếu máu về 0 thì tiêu diệt
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        // Hủy đối tượng (kẻ địch bị tiêu diệt)
        Destroy(gameObject);
    }
}
