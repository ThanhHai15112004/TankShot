using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField] private PatrolPath patrolPath; // Đường dẫn tuần tra
    [SerializeField] private float moveSpeed = 3f; // Tốc độ di chuyển
    [SerializeField] private float rotationSpeed = 100f; // Tốc độ quay của thân xe tăng
    [SerializeField] private float turretRotationSpeed = 150f; // Tốc độ quay của nòng súng
    [SerializeField] private float arriveDistance = 0.5f; // Khoảng cách tối thiểu để coi là đã đến điểm tuần tra

    [Header("Combat Settings")]
    [SerializeField] private AIDetector aiDetector; // Hệ thống phát hiện mục tiêu
    [SerializeField] private TankShooting tankShooting; // Hệ thống bắn

    [Header("References")]
    [SerializeField] private Transform turret; // Nòng súng (Turret)

    private int currentPointIndex = 0; // Chỉ số điểm tuần tra hiện tại
    private int direction = 1; // Hướng di chuyển (1 = tiến tới, -1 = lùi lại)

    private void Start()
    {
        if (patrolPath == null || patrolPath.PointCount == 0)
        {
            Debug.LogError("PatrolPath is not set or has no points!");
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        if (aiDetector != null && aiDetector.DetectedTarget != null)
        {
            EngageTarget(aiDetector.DetectedTarget);
        }
        else
        {
            Patrol();
        }
        RotateTurret(); // Quay nòng súng
    }

    private void Patrol()
    {
        if (patrolPath.PointCount == 0) return;

        Transform targetPoint = patrolPath.GetPoint(currentPointIndex);
        if (targetPoint == null) return;

        // Hướng di chuyển đến điểm tuần tra
        Vector2 directionToTarget = (targetPoint.position - transform.position).normalized;

        // Quay thân xe tăng về phía điểm tuần tra
        RotateBody(directionToTarget);

        // Di chuyển về phía điểm tuần tra
        if (IsFacingTarget(directionToTarget)) // Chỉ di chuyển khi thân xe tăng đã quay đúng hướng
        {
            transform.position += transform.up * moveSpeed * Time.deltaTime;
        }

        // Kiểm tra nếu đã đến gần điểm tuần tra
        if (Vector2.Distance(transform.position, targetPoint.position) <= arriveDistance)
        {
            // Chuyển sang điểm tiếp theo theo hướng hiện tại
            currentPointIndex += direction;

            // Đảo chiều khi đạt đến điểm cuối hoặc đầu
            if (currentPointIndex >= patrolPath.PointCount || currentPointIndex < 0)
            {
                direction *= -1;
                currentPointIndex += direction;
            }
        }
    }

    private void RotateBody(Vector2 directionToTarget)
    {
        float targetAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90f;
        float currentAngle = transform.eulerAngles.z;
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, newAngle);
    }

    private bool IsFacingTarget(Vector2 directionToTarget)
    {
        float dotProduct = Vector2.Dot(transform.up, directionToTarget);
        return dotProduct > 0.95f; // Gần như đối diện
    }

    private void RotateTurret()
    {
        if (turret == null) return;

        Transform target = aiDetector?.DetectedTarget ?? patrolPath.GetPoint(currentPointIndex);
        if (target == null) return;

        // Hướng nòng súng về phía mục tiêu hoặc điểm tuần tra
        Vector2 directionToTarget = (target.position - turret.position).normalized;
        float targetAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90f;

        float currentAngle = turret.eulerAngles.z;
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, turretRotationSpeed * Time.deltaTime);
        turret.rotation = Quaternion.Euler(0, 0, newAngle);
    }

    private void EngageTarget(Transform target)
    {
        // Quay thân xe tăng về phía mục tiêu
        Vector2 directionToTarget = (target.position - transform.position).normalized;
        RotateBody(directionToTarget);

        // Chỉ bắn nếu thân xe tăng đã quay đúng hướng
        if (IsFacingTarget(directionToTarget))
        {
            tankShooting.Shoot();
        }
    }
}
