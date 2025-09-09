using UnityEngine;

public class JumpTrajectory
{
    private Vector3 start;
    private Vector3 target;
    private readonly float maxHeight;
    private float duration;
    private float elapsed;
    private float maxHaingt;

    public float MaxHainght;

    public bool Active => elapsed < duration;

    public JumpTrajectory(float maxHeight)
    {
        this.maxHeight = maxHeight;
        elapsed = 0f;
    }

    public void Initialize(Vector3 startPosition, Vector3 moveDirection, float jumpDistance, float jumpDuration)
    {
        start = startPosition;
        var horizontalDir = moveDirection;
        horizontalDir.y = 0f;
        if (horizontalDir.sqrMagnitude < 0.001f)
            horizontalDir = Vector3.forward;

        target = start + horizontalDir.normalized * jumpDistance;
        duration = jumpDuration;
        elapsed = 0f;
    }

    public Vector3 Step(float deltaTime)
    {
        elapsed += deltaTime;
        var t = Mathf.Clamp01(elapsed / duration);

        var horizontal = Vector3.Lerp(start, target, t);
        var y = Mathf.Lerp(start.y, target.y, t) + Mathf.Sin(t * Mathf.PI) * maxHeight;

        return new Vector3(horizontal.x, y, horizontal.z);
    }
}