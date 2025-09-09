using UnityEngine;

public class ChargingState : IPlayerState
{
    public event System.Action<IPlayerState> OnStateRequest;
    public event System.Action OnVolumeEmpty;

    private PlayerModel model;
    private PlayerView view;
    private ProjectileController projectile;
    private ProjectileFactory factory;
    private Transform target;
    private PlayerPathView playerPathView;
    private IPlayerState nextState;

    public ChargingState(ProjectileFactory factory, Transform target)
    {
        this.factory = factory;
        this.target = target;
    }

    public void Initialize(PlayerModel model, PlayerView view)
    {
        this.model = model;
        this.view = view;
    }

    public void SetNextState(IPlayerState state)
    {
        nextState = state;
    }

    public void SetPathView(PlayerPathView playerPathView)
    {
        this.playerPathView = playerPathView;
    }

    public void Enter()
    {
        if (projectile == null)
            projectile = factory.Create(model.Position, target.position);
    }

    public void Tick(float deltaTime)
    {
        HandleCharging(deltaTime);
        HandleRelease();
    }

    private void HandleCharging(float deltaTime)
    {
        if (projectile == null) return;

        float oldRadius = model.CurrentSize;
        float deltaRadius = model.Charge(deltaTime * model.Multipler);

        if (deltaRadius <= 0f)
        {
            Reset();
            OnVolumeEmpty?.Invoke();
            OnStateRequest?.Invoke(nextState);
            return;
        }

        float newPlayerRadius = model.CurrentSize;
        float lostVolume = VolumeCalculator.RadiusToVolume(oldRadius) - VolumeCalculator.RadiusToVolume(newPlayerRadius);

        float projectileVolume = VolumeCalculator.GetVolume(projectile.Size);
        float newProjectileVolume = projectileVolume + lostVolume;

        model.SetSize(newPlayerRadius);
        model.SetPosition(new Vector3(model.Position.x, model.CurrentSize / 2f, model.Position.z));
        view.SetSize(model.CurrentSize);
        view.SetPosition(model.Position);

        projectile.SetSize(VolumeCalculator.VolumeToRadius(newProjectileVolume));

        if (playerPathView != null)
            playerPathView.SetWidth(model.CurrentSize);
    }



    private void HandleRelease()
    {
        if (Input.GetMouseButtonUp(0) && projectile != null)
        {
            projectile.Release(target.position);
            projectile = null;
            OnStateRequest?.Invoke(nextState);
        }
    }

    public void Reset()
    {
        if (projectile != null)
        {
            projectile.Destroy();
            projectile = null;
        }
        view.SetSize(model.CurrentSize);
        playerPathView.SetWidth(model.CurrentSize);
    }

    public void Exit() { }
}