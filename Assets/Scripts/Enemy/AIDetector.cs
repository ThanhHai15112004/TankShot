using System.Collections;
using UnityEngine;

public class AIDetector : MonoBehaviour
{
    [Header("Detection Settings")]
    [Range(1, 15)]
    [SerializeField] private float viewRadius = 11;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float detectionInterval = 0.2f;

    [Header("Debug Options")]
    [SerializeField] private bool showDebugGizmos = true;

    public Transform DetectedTarget { get; private set; }

    private void Start()
    {
        StartCoroutine(DetectionRoutine());
    }

    private IEnumerator DetectionRoutine()
    {
        while (true)
        {
            DetectTarget();
            yield return new WaitForSeconds(detectionInterval);
        }
    }

    private void DetectTarget()
    {
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetLayer);
        DetectedTarget = null;

        foreach (var target in targetsInViewRadius)
        {
            Vector2 directionToTarget = (target.transform.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToTarget, viewRadius, obstacleLayer);

            if (hit.collider == null) // Không có vật cản chắn
            {
                DetectedTarget = target.transform;
                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!showDebugGizmos) return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        if (DetectedTarget != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, DetectedTarget.position);
        }
    }
}
