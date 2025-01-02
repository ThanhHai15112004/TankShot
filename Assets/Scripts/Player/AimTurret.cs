using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AimTurret : MonoBehaviour
{
    [Header("Turret Settings")]
    public float turretRotationSpeed = 150f;
    private Transform target;

    public void SetTarget(Transform enemyTarget)
    {
        target = enemyTarget;
    }
    private void Update()
    {
        if (target == null) return;

        Vector2 direction = (target.position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        float currentAngle = transform.eulerAngles.z;
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, turretRotationSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0, 0, newAngle);
    }

    public void AimAtTarget(Vector3 position)
    {
        Vector2 direction = (position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        float currentAngle = transform.eulerAngles.z;
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, turretRotationSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0, 0, newAngle);
    }
}