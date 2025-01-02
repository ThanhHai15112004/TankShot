using UnityEngine;
using System.Linq;

public class AutoAimTurret : MonoBehaviour
{
    [Header("Settings")]
    public float rotationSpeed = 150f; // Tốc độ quay của turret
    public float detectionRange = 10f; // Phạm vi phát hiện enemy
    public string enemyTag = "Enemy"; // Tag của enemy

    private Transform currentTarget;

    void Update()
    {
        FindClosestEnemy();

        if (currentTarget != null)
        {
            AimAtTarget(currentTarget);
        }
    }

    private void FindClosestEnemy()
    {
        // Tìm tất cả enemy trong phạm vi
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        if (enemies.Length == 0)
        {
            currentTarget = null;
            return;
        }

        // Lọc những enemy trong phạm vi và lấy enemy gần nhất
        currentTarget = enemies
            .Select(enemy => enemy.transform)
            .Where(enemy => Vector2.Distance(transform.position, enemy.position) <= detectionRange)
            .OrderBy(enemy => Vector2.Distance(transform.position, enemy.position))
            .FirstOrDefault();
    }

    private void AimAtTarget(Transform target)
    {
        if (target == null) return;

        // Tính toán góc cần quay
        Vector2 direction = (target.position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Lấy góc hiện tại của turret
        float currentAngle = transform.eulerAngles.z;

        // Quay từ góc hiện tại tới góc mục tiêu
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);

        // Cập nhật góc quay
        transform.rotation = Quaternion.Euler(0, 0, newAngle);
    }

    private void OnDrawGizmosSelected()
    {
        // Hiển thị phạm vi phát hiện để debug
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
