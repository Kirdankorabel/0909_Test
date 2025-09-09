using UnityEngine;
using System.Collections.Generic;

public class ObstacleController
{
    private static float obstacleSize = 0.5f;

    private static List<ObstacleView> obstacles = new List<ObstacleView>();
    private static List<Vector3> positions = new List<Vector3>();

    private GameObject obstaclePrefab;
    private Transform root;
    private int count;
    private Vector3 areaMin;
    private Vector3 areaMax;

    private Vector3 clearCenter = Vector3.zero;
    private float clearRadius = 0f;
    private bool useClearZone = false;

    public ObstacleController SetPrefab(GameObject prefab)
    {
        obstaclePrefab = prefab;
        return this;
    }

    public ObstacleController SetRoot(Transform rootTransform)
    {
        root = rootTransform;
        return this;
    }

    public ObstacleController SetCount(int obstacleCount)
    {
        count = obstacleCount;
        return this;
    }

    public ObstacleController SetArea(Vector3 min, Vector3 max)
    {
        areaMin = min;
        areaMax = max;
        return this;
    }

    public ObstacleController SetClearCenter(Vector3 center, float radius)
    {
        clearCenter = center;
        clearRadius = radius;
        useClearZone = true;
        return this;
    }

    public ObstacleController Build()
    {
        for (int i = 0; i < count; i++)
        {
            var obj = Object.Instantiate(obstaclePrefab, Vector3.zero, Quaternion.identity, root);
            var view = obj.GetComponent<ObstacleView>();
            view.SetInactive();

            obstacles.Add(view);
            positions.Add(Vector3.zero);
        }

        ShufflePositions();
        ActivateAll();

        return this;
    }

    public void ActivateAll()
    {
        foreach (var obs in obstacles)
            obs.SetActive();
    }

    public void ShufflePositions()
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            Vector3 newPos;
            int attempts = 0;

            do
            {
                newPos = new Vector3(
                    Random.Range(areaMin.x, areaMax.x),
                    Random.Range(areaMin.y, areaMax.y),
                    Random.Range(areaMin.z, areaMax.z)
                );
                attempts++;
            }
            while (useClearZone && Vector3.Distance(new Vector3(newPos.x, 0f, newPos.z), new Vector3(clearCenter.x, 0f, clearCenter.z)) < clearRadius && attempts < 50);

            obstacles[i].SetPosition(newPos);
            positions[i] = newPos;
        }
    }

    public static void DeactivateInRadius(Vector3 center, float radius)
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            if (Vector3.Distance(positions[i], center) <= radius)
                obstacles[i].Infect();
        }
    }

    public static bool IsFreeArea(Vector3 position, Vector3 direction, float width, float length)
    {
        var dirNorm = direction.normalized;
        var right = Vector3.Cross(Vector3.up, dirNorm);

        for (int i = 0; i < obstacles.Count; i++)
        {
            if (!obstacles[i].IsAlive) continue;

            var toObs = positions[i] - position;
            var forwardDist = Vector3.Dot(toObs, dirNorm);
            if (forwardDist < -obstacleSize || forwardDist > length + obstacleSize) continue;

            var lateralDist = Mathf.Abs(Vector3.Dot(toObs, right));
            float safeRadius = width / 2f + obstacleSize;

            if (lateralDist <= safeRadius)
                return false;
        }

        return true;
    }
}