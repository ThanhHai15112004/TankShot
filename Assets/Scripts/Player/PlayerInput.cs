using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;


    public UnityEvent OnShoot = new UnityEvent();
    public UnityEvent<Vector2> OnMoveBody = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> OnMoveTurret = new UnityEvent<Vector2>();

    private bool isMobile;

    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }


    }

    private void Update()
    {
        
        
        GetPCInput();
        
    }

    private void GetPCInput()
    {
        // Di chuyển bằng phím
        Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        OnMoveBody?.Invoke(movementVector.normalized);

        // Điều khiển nòng súng bằng chuột
        OnMoveTurret?.Invoke(GetMousePosition());

        // Bắn bằng chuột
        if (Input.GetMouseButtonDown(0))
        {
            OnShoot?.Invoke();
        }
    }


    private Vector2 GetMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.nearClipPlane;
        return mainCamera.ScreenToWorldPoint(mousePosition);
    }

}
