using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "BallGame/GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] private float startSize = 1f;
    [SerializeField] private float minSize = 0.2f;
    [SerializeField] private float chargeRate = 0.5f;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float explosionMultiplier = 2f;
    [SerializeField] private float doorOpenDistance = 5f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpHaingt;
    [SerializeField] private float multipler = 1f;

    public float StartSize => startSize;
    public float MinSize => minSize;
    public float ChargeRate => chargeRate;
    public float ProjectileSpeed => projectileSpeed;
    public float ExplosionMultiplier => explosionMultiplier;
    public float DoorOpenDistance => doorOpenDistance;
    public float MoveSpeed => moveSpeed;
    public float JumpHaingt => jumpHaingt;
    public float Multipler => multipler;
}