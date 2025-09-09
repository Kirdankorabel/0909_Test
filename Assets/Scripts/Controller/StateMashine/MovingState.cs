using UnityEngine;

public class MovingState : IPlayerState
{
    public event System.Action<IPlayerState> OnStateRequest;

    private PlayerModel model;
    private PlayerView view;
    private Transform target;
    private JumpTrajectory jump;
    private float jumpHeight = 2f;
    private float jumpDuration = 1f;

    private IPlayerState nextState;

    public Transform Target => target;

    public MovingState(Transform target)
    {
        this.target = target;
        jump = new JumpTrajectory(jumpHeight);
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

    public void Enter()
    {
        var direction = target.position - model.Position;
        direction.y = 0f;
        jump.Initialize(model.Position, direction, jumpHeight, jumpDuration);
    }

    public void Tick(float deltaTime)
    {
        if (!jump.Active)
        {
            OnStateRequest?.Invoke(nextState);
            return;
        }

        var nextPos = jump.Step(deltaTime);
        model.SetPosition(nextPos);
        view.SetPosition(nextPos);
    }

    public void Exit() { }
}