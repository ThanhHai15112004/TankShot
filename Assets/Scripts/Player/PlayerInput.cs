using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    public UnityEvent OnShoot = new UnityEvent();
    public UnityEvent<Vector2> OnMoveBody = new UnityEvent<Vector2>();

    [Header("Mobile Controls")]
    public DynamicJoystick joystick; // Joystick di chuyển

    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Update()
    {
        if (Application.isMobilePlatform)
        {
            GetMobileInput();
        }
        else
        {
            GetPCInput();
        }
    }

    private void GetMobileInput()
    {
        // Lấy input từ joystick
        Vector2 movementVector = new Vector2(joystick.Horizontal, joystick.Vertical);
        OnMoveBody?.Invoke(movementVector.normalized);

    }

    private void GetPCInput()
    {
        Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        OnMoveBody?.Invoke(movementVector.normalized);

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Shoot button pressed");
            OnShoot?.Invoke();
        }
    }
}