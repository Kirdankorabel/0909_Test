using UnityEngine;

public class PlayerInstaller : MonoBehaviour
{
    [SerializeField] private GameSettings settings;
    [SerializeField] private PlayerView playerView;
    [SerializeField] private PlayerPathView playerPathView;
    [SerializeField] private ProjectileView projectilePrefab;
    [SerializeField] private TargetView target;
    [SerializeField] private Transform startPoint;
    [SerializeField] private ParticleSystem myParticleSystem;

    private PlayerController playerController;
    private ProjectileFactory projectileFactory;

    void Awake()
    {
        projectileFactory = new ProjectileFactory(settings, projectilePrefab, myParticleSystem);

        var playerModel = new PlayerModel(settings.StartSize, settings.MinSize, settings.ChargeRate, settings.MoveSpeed, settings.Multipler);
        playerController = new PlayerController(playerModel, playerView, projectileFactory)
            .SetTarget(target)
            .SetStates()
            .SetPathView(playerPathView)
            .Build();
        playerController.SetStartPosition(startPoint.position);
        playerController.Reset();

        Main.SetPlayerController(playerController);
    }
}