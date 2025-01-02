using System.Collections;
using UnityEngine;

public class TankTrackSpawner : MonoBehaviour
{
    private Vector2 lastPosition;
    public float trackDistance = 0.2f; // Khoảng cách giữa các track
    public GameObject trackPrefab;
    public int objectPoolSize = 50;
    public Vector3 trackOffset = new Vector3(0, -0.5f, 0); // Offset vị trí track

    private ObjectPool objectPool;

    private void Awake()
    {
        objectPool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        lastPosition = transform.position;
        objectPool.Initialize(trackPrefab, objectPoolSize);
    }

    private void Update()
    {
        // Kiểm tra khoảng cách di chuyển
        var distanceDriven = Vector2.Distance(transform.position, lastPosition);
        if (distanceDriven >= trackDistance)
        {
            lastPosition = transform.position;

            // Lấy track từ Object Pool
            var tracks = objectPool.CreateObject();

            // Cập nhật vị trí và góc quay của track
            tracks.transform.position = transform.position + transform.TransformDirection(trackOffset);
            tracks.transform.rotation = transform.rotation;
        }
    }
}
