using UnityEngine;

public class TankAI : MonoBehaviour
{
    private AIDetector aiDetector;
    private AimTurret aimTurret;
    private TankShooting tankShooting;

    [SerializeField] private float aimTolerance = 10f; // Góc chênh lệch tối đa để bắn

    private void Awake()
    {
        aiDetector = GetComponentInChildren<AIDetector>();
        aimTurret = GetComponentInChildren<AimTurret>();
        tankShooting = GetComponentInChildren<TankShooting>();

        // Đảm bảo AI sử dụng TankShooting
        if (tankShooting != null)
        {
            tankShooting.IsPlayerControlled = false;
        }
    }

    private void Update()
    {
        if (aiDetector.DetectedTarget != null)
        {
            aimTurret.AimAtTarget(aiDetector.DetectedTarget);

            if (IsTurretAimed(aiDetector.DetectedTarget.position))
            {
                tankShooting.Shoot();
            }
        }
    }

    private bool IsTurretAimed(Vector2 targetPosition)
    {
        Vector2 directionToTarget = (targetPosition - (Vector2)aimTurret.transform.position).normalized;
        float dotProduct = Vector2.Dot(aimTurret.transform.up, directionToTarget);

        // Dot Product gần 1 có nghĩa là nòng súng gần như hướng đúng
        return dotProduct > Mathf.Cos(aimTolerance * Mathf.Deg2Rad);
    }
}