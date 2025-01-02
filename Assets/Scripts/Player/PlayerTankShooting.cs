using UnityEngine;

public class PlayerTankShooting : TankShooting
{
    public override void Shoot()
    {
        if (!canShoot) return;
        base.Shoot();
    }
}
