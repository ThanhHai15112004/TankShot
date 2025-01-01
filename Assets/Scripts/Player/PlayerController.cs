using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public TankMover tankMover;
    public AimTurret aimTurret;
    public PlayerInput playerInput;
    [SerializeField] private TankShooting tankShooting;

    private void Awake()
    {
        if (playerInput != null)
        {
            playerInput.OnMoveBody.AddListener(HandleMovement);
            playerInput.OnMoveTurret.AddListener(HandleTurretRotation);
            playerInput.OnShoot.AddListener(HandleShooting);
        }

        // Đảm bảo chỉ PlayerController sử dụng TankShooting
        if (tankShooting != null)
        {
            tankShooting.IsPlayerControlled = true;
        }
    }

    public void HandleMovement(Vector2 movementVector)
    {
        if (tankMover != null)
        {
            tankMover.Move(movementVector);
        }
    }

    public void HandleTurretRotation(Vector2 pointerPosition)
    {
        if (aimTurret != null)
        {
            aimTurret.Aim(pointerPosition);
        }
    }

    public void HandleShooting()
    {
        if (tankShooting != null)
        {
            tankShooting.Shoot();
        }
    }
}