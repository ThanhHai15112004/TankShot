using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private AIBehaviour patrolBehaviour;
    [SerializeField] private AIBehaviour shootBehaviour;
    [SerializeField] private PlayerController tank;
    [SerializeField] private AIDetector detector;

    private void Update()
    {
        if (detector.DetectedTarget != null)
        {
            shootBehaviour.PerformAction(tank, detector);
        }
        else
        {
            patrolBehaviour.PerformAction(tank, detector);
        }
    }
}