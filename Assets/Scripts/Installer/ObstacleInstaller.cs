using UnityEngine;

public class ObstacleInstaller : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private int obstacleCount = 20;
    [SerializeField] private Vector3 areaMin;
    [SerializeField] private Vector3 areaMax;
    [SerializeField] private float centerClearRadius = 5f;

    private ObstacleController obstacleController;

    void Awake()
    {
        obstacleController = new ObstacleController()
            .SetPrefab(obstaclePrefab)
            .SetRoot(transform)
            .SetCount(obstacleCount)
            .SetArea(areaMin, areaMax)
            .SetClearCenter(Vector3.zero, centerClearRadius)
            .Build();

        Main.SetObstacleController(obstacleController);
    }
}