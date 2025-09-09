using UnityEngine;

public class IdleState : IPlayerState
{
    public event System.Action<IPlayerState> OnStateRequest;

    private PlayerModel model;
    private PlayerView view;
    private IPlayerState nextMoveState;
    private IPlayerState nextChargeState;

    private float checkDistance = 5f;

    public void Initialize(PlayerModel model, PlayerView view)
    {
        this.model = model;
        this.view = view;
    }

    public void SetNextStates(IPlayerState moveState, IPlayerState chargeState)
    {
        nextMoveState = moveState;
        nextChargeState = chargeState;
    }

    public void Enter() { }

    public void Tick(float deltaTime)
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnStateRequest?.Invoke(nextChargeState);
        }
        else if (ObstacleController.IsFreeArea(model.Position, Vector3.forward, model.CurrentSize, checkDistance))
        {
            OnStateRequest?.Invoke(nextMoveState);
        }
    }

    public void Exit() { }
}