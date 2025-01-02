using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField] private PatrolPath patrolPath;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float turretRotationSpeed = 150f;
    [SerializeField] private float arriveDistance = 0.5f;

    [Header("Combat Settings")]
    [SerializeField] private AIDetector aiDetector;
    [SerializeField] private TankShooting tankShooting;
    [SerializeField] private float shootingRange = 8f;

    [Header("References")]
    [SerializeField] private Transform turret;

    private int currentPointIndex = 0;
    private int direction = 1;

    private void Start()
    {
        if (patrolPath == null || patrolPath.PointCount == 0)
        {
            Debug.LogError("PatrolPath is not set or has no points!");
            enabled = false;
        }
    }

    private void Update()
    {
        if (aiDetector.DetectedTarget != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, aiDetector.DetectedTarget.position);
            if (distanceToTarget <= shootingRange)
            {
                EngageTarget(aiDetector.DetectedTarget);
            }
            else
            {
                ChaseTarget(aiDetector.DetectedTarget);
            }
        }
        else if (aiDetector.ChaseTarget != null)
        {
            ChaseTarget(aiDetector.ChaseTarget);
        }
        else
        {
            Patrol();
        }

        RotateTurret();
    }

    private void Patrol()
    {
        if (patrolPath == null || patrolPath.PointCount == 0) return;

        Transform targetPoint = patrolPath.GetPoint(currentPointIndex);
        if (targetPoint == null) return;

        Vector2 directionToTarget = (targetPoint.position - transform.position).normalized;
        RotateBody(directionToTarget);

        if (IsFacingTarget(directionToTarget))
        {
            transform.position += transform.up * moveSpeed * Time.deltaTime;
        }

        if (Vector2.Distance(transform.position, targetPoint.position) <= arriveDistance)
        {
            currentPointIndex += direction;

            if (currentPointIndex >= patrolPath.PointCount || currentPointIndex < 0)
            {
                direction *= -1;
                currentPointIndex += direction;
            }
        }
    }

    private void ChaseTarget(Transform target)
    {
        if (target == null) return;

        Vector2 directionToTarget = (target.position - transform.position).normalized;
        RotateBody(directionToTarget);

        if (IsFacingTarget(directionToTarget))
        {
            transform.position += transform.up * moveSpeed * Time.deltaTime;
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
        return Vector2.Dot(transform.up, directionToTarget) > 0.95f;
    }

    private void RotateTurret()
    {
        if (turret == null) return;

        Transform target = aiDetector?.DetectedTarget ?? patrolPath.GetPoint(currentPointIndex);
        if (target == null) return;

        Vector2 directionToTarget = (target.position - turret.position).normalized;
        float targetAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90f;

        float currentAngle = turret.eulerAngles.z;
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, turretRotationSpeed * Time.deltaTime);
        turret.rotation = Quaternion.Euler(0, 0, newAngle);
    }

    private void EngageTarget(Transform target)
    {
        if (target == null) return;

        Vector2 directionToTarget = (target.position - transform.position).normalized;
        RotateBody(directionToTarget);

        if (IsFacingTarget(directionToTarget))
        {
            tankShooting.Shoot();
        }
    }
}