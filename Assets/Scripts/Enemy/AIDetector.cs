using UnityEngine;

public class AIDetector : MonoBehaviour
{
    [Range(1, 15)][SerializeField] private float viewRadius = 11;
    [Range(1, 20)][SerializeField] private float chaseRadius = 15;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private LayerMask obstacleLayer;

    public Transform DetectedTarget { get; private set; }
    public Transform ChaseTarget { get; private set; }

    private void Update()
    {
        DetectTarget();
    }

    private void DetectTarget()
    {
        DetectedTarget = GetTargetInRange(viewRadius);
    }

    private Transform GetTargetInRange(float radius)
    {
        Collider2D[] targetsInRange = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);
        foreach (var target in targetsInRange)
        {
            Vector2 directionToTarget = (target.transform.position - transform.position).normalized;
            float distanceToTarget = Vector2.Distance(transform.position, target.transform.position);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleLayer);

            if (hit.collider == null)
            {
                return target.transform;
            }
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);

        if (DetectedTarget != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, DetectedTarget.position);
        }

        if (ChaseTarget != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, ChaseTarget.position);
        }
    }
}