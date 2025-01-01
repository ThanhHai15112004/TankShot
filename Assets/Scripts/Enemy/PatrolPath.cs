using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    public List<Transform> patrolPoints = new List<Transform>(); // Danh sách các điểm tuần tra

    [Header("Gizmos Settings")]
    public Color pathColor = Color.green; // Màu của đường nối giữa các điểm
    public float pointSize = 0.3f; // Kích thước của điểm tuần tra

    public int PointCount => patrolPoints.Count; // Số lượng điểm tuần tra

    public Transform GetPoint(int index)
    {
        if (index < 0 || index >= patrolPoints.Count)
        {
            Debug.LogWarning("Invalid patrol point index!");
            return null;
        }
        return patrolPoints[index];
    }

    private void OnDrawGizmos()
    {
        if (patrolPoints.Count == 0) return;

        for (int i = 0; i < patrolPoints.Count; i++)
        {
            // Vẽ điểm tuần tra
            Gizmos.color = pathColor;
            Gizmos.DrawSphere(patrolPoints[i].position, pointSize);

            // Vẽ đường nối giữa các điểm
            if (i < patrolPoints.Count - 1)
            {
                Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i + 1].position);
            }
            else
            {
                // Kết nối điểm cuối với điểm đầu để tạo vòng lặp
                Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[0].position);
            }
        }
    }
}
