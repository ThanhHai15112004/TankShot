using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public TankMover tankMover;
    public AimTurret aimTurret;
    public PlayerInput playerInput;
    [SerializeField] private TankShooting tankShooting;
    [SerializeField] private AIDetector aiDetector;

    private void Awake()
    {
        if (playerInput != null)
        {
            playerInput.OnMoveBody.AddListener(HandleMovement);
            playerInput.OnShoot.AddListener(HandleShooting);
        }

        if (aiDetector != null)
        {
            StartCoroutine(TrackEnemy());
        }
    }

    public void HandleMovement(Vector2 movementVector)
    {
        if (tankMover != null)
        {
            tankMover.Move(movementVector);
        }
    }

    public void HandleShooting()
    {
        if (tankShooting != null)
        {
            tankShooting.Shoot();
        }
    }

    public IEnumerator TrackEnemy()
    {
        while (true)
        {
            if (aiDetector != null && aiDetector.DetectedTarget != null)
            {
                aimTurret.SetTarget(aiDetector.DetectedTarget); // Chỉ xoay nòng súng
            }
            else
            {
                aimTurret.SetTarget(null);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
