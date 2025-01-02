using UnityEngine;

public class AIPatrolBehaviour : AIBehaviour
{
    public PatrolPath patrolPath;
    private int currentPointIndex = 0;

    public override void PerformAction(PlayerController tank, AIDetector detector)
    {
        if (patrolPath == null || patrolPath.PointCount == 0) return;

        Transform targetPoint = patrolPath.GetPoint(currentPointIndex);
        if (Vector2.Distance(tank.transform.position, targetPoint.position) < 0.5f)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPath.PointCount;
        }
        else
        {
            Vector2 direction = (targetPoint.position - tank.transform.position).normalized;
            tank.tankMover.Move(direction);
        }
    }
}