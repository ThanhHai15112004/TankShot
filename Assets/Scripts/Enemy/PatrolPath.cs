using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    public List<Transform> patrolPoints = new List<Transform>();
    public int PointCount => patrolPoints.Count;

    public Transform GetPoint(int index)
    {
        return (index >= 0 && index < patrolPoints.Count) ? patrolPoints[index] : null;
    }

    private void OnDrawGizmos()
    {
        if (patrolPoints.Count == 0) return;

        for (int i = 0; i < patrolPoints.Count; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(patrolPoints[i].position, 0.3f);

            if (i > 0)
                Gizmos.DrawLine(patrolPoints[i - 1].position, patrolPoints[i].position);
        }

        Gizmos.DrawLine(patrolPoints[patrolPoints.Count - 1].position, patrolPoints[0].position);
    }
}