using UnityEngine;

public class AimTurret : MonoBehaviour
{
    [Header("Turret Settings")]
    public float turretRotationSpeed = 150f;

    public void Aim(Vector2 pointerPosition)
    {
        Vector2 turretDirection = pointerPosition - (Vector2)transform.position;

        if (turretDirection.sqrMagnitude < Mathf.Epsilon) return;

        float desiredAngle = Mathf.Atan2(turretDirection.y, turretDirection.x) * Mathf.Rad2Deg - 90f;
        float rotationStep = turretRotationSpeed * Time.deltaTime;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, desiredAngle), rotationStep);
    }

    public void AimAtTarget(Transform target)
    {
        if (target == null) return;

        Vector2 direction = (target.position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        float currentAngle = transform.eulerAngles.z;
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, turretRotationSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0, 0, newAngle);
    }
}
