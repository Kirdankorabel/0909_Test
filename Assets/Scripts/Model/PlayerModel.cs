using UnityEngine;

public class PlayerModel
{
    public float CurrentSize { get; private set; }
    public float MinSize { get; }
    public float ChargeRate { get; }
    public float Multipler { get; }
    public float MoveSpeed { get; }
    public float StartSize { get; private set; }
    public Vector3 Position { get; private set; }
    public Vector3 StartPosition { get; private set; }

    public PlayerModel(float startSize, float minSize, float chargeRate, float moveSpeed, float multipler)
    {
        this.StartSize = startSize;
        CurrentSize = startSize;
        MinSize = minSize;
        ChargeRate = chargeRate;
        MoveSpeed = moveSpeed;
        Multipler = multipler;
    }

    public void Reset()
    {
        CurrentSize = StartSize;
        Position = StartPosition;
    }

    public float Charge(float dt)
    {
        float growth = ChargeRate * dt;
        if (CurrentSize - growth > MinSize)
        {
            CurrentSize -= growth;
            return growth;
        }
        return 0;
    }

    public void SetSize(float size)
    {
        CurrentSize = size;
    }

    public void SetPosition(Vector3 position)
    {
        Position = position;
    }

    public void SetStartPosition(Vector3 startPosition)
    {
        StartPosition = startPosition;
    }
}