using UnityEngine;

public class ProjectileModel
{
    public Vector3 Position { get; private set; }
    public float Radius { get; private set; }
    public bool IsReleased { get; private set; }
    public Vector3 Direction { get; private set; }

    public ProjectileModel(Vector3 startPos, Vector3 targetPos, float radius)
    {
        Position = startPos;
        Radius = radius;
        IsReleased = false;

        var direction = (targetPos - Position).normalized;
        direction.y = 0;
        Direction = direction.normalized;
    }

    public void Release()
    {
        IsReleased = true;
    }

    public void SetPosition(Vector3 newPos)
    {
        Position = newPos;
    }

    public void SetRadius(float radius)
    {
        Radius = radius;
    }
}