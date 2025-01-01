using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TankMovementData
{
    public float acceleration = 5f;
    public float deacceleration = 5f;
    public float maxSpeed = 10f;
    public float rotationSpeed = 100f;
}

public class TankMover : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public TankMovementData movementData;

    private Vector2 movementVector;
    private float currentSpeed = 0f;
    private float currentDirection = 1f;

    public UnityEvent<float> OnSpeedChange = new UnityEvent<float>();

    private void Awake()
    {
        rb2d = GetComponentInParent<Rigidbody2D>();
    }

    public void Move(Vector2 movementVector)
    {
        this.movementVector = movementVector;
        UpdateSpeed(movementVector);
        OnSpeedChange?.Invoke(currentSpeed);
        UpdateDirection(movementVector.y);
    }

    private void UpdateSpeed(Vector2 movementVector)
    {
        if (Mathf.Abs(movementVector.y) > 0)
        {
            currentSpeed += movementData.acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed -= movementData.deacceleration * Time.deltaTime;
        }

        currentSpeed = Mathf.Clamp(currentSpeed, 0, movementData.maxSpeed);
    }

    private void UpdateDirection(float movementInputY)
    {
        if (movementInputY > 0)
        {
            if (currentDirection == -1) currentSpeed = 0;
            currentDirection = 1;
        }
        else if (movementInputY < 0)
        {
            if (currentDirection == 1) currentSpeed = 0;
            currentDirection = -1;
        }
    }

    private void FixedUpdate()
    {
        rb2d.linearVelocity = transform.up * currentSpeed * currentDirection;

        float rotationAmount = -movementVector.x * movementData.rotationSpeed * Time.fixedDeltaTime;
        rb2d.MoveRotation(rb2d.rotation + rotationAmount);
    }
}
