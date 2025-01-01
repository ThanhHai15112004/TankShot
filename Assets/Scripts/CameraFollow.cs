using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target; // Đối tượng mà camera theo dõi (Player)
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10); // Khoảng cách của camera so với Player

    private void Update()
    {
        if (target == null) return;

        // Camera luôn theo dõi vị trí của Player với khoảng cách cố định
        transform.position = target.position + offset;
    }
}
