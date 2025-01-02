using UnityEngine;

public class AIShootBehaviour : AIBehaviour
{
    public override void PerformAction(PlayerController tank, AIDetector detector)
    {
        if (detector.DetectedTarget == null) return;

        tank.aimTurret.AimAtTarget(detector.DetectedTarget.position);

        if (Vector2.Distance(tank.transform.position, detector.DetectedTarget.position) < 8f)
        {
            tank.HandleShooting();
        }
    }
}