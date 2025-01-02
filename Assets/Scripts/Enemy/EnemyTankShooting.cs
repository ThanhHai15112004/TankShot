using UnityEngine;

public class EnemyTankShooting : TankShooting
{
    [SerializeField] private AIDetector aiDetector;

    private void Update()
    {
        if (aiDetector != null && aiDetector.DetectedTarget != null && canShoot)
        {
            AimAndShoot();
        }
    }

    private void AimAndShoot()
    {
        if (aiDetector.DetectedTarget == null) return;

        aimTurret.AimAtTarget(aiDetector.DetectedTarget.position);

        if (Vector2.Distance(transform.position, aiDetector.DetectedTarget.position) < 8f)
        {
            Shoot();
        }
    }

    public override void Shoot()
    {
        if (!canShoot) return;
        base.Shoot();
    }
}
