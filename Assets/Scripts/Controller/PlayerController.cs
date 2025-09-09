using UnityEngine;

public class PlayerController : ITickListener
{
    public event System.Action<float> OnWin;

    private IPlayerState currentState;
    private IdleState idleState;
    private ChargingState chargingState;
    private MovingState movingState;

    private PlayerModel model;
    private PlayerView view;
    private ProjectileFactory factory;
    private TargetView target;

    private bool gameEnded;

    public PlayerController(PlayerModel model, PlayerView view, ProjectileFactory factory)
    {
        this.model = model;
        this.view = view;
        this.factory = factory;
    }

    public PlayerController SetTarget(TargetView target)
    {
        this.target = target;
        return this;
    }

    public PlayerController SetStates()
    {
        idleState = new IdleState();
        chargingState = new ChargingState(factory, target.transform);
        movingState = new MovingState(target.transform);

        idleState.Initialize(model, view);
        chargingState.Initialize(model, view);
        movingState.Initialize(model, view);

        idleState.SetNextStates(movingState, chargingState);
        chargingState.SetNextState(idleState);
        movingState.SetNextState(idleState);

        idleState.OnStateRequest += ChangeState;
        chargingState.OnStateRequest += ChangeState;
        movingState.OnStateRequest += ChangeState;

        chargingState.OnVolumeEmpty += Lose;

        return this;
    }

    public PlayerController SetPathView(PlayerPathView pathView)
    {
        chargingState.SetPathView(pathView);
        pathView.SetWidth(model.StartSize);
        return this;
    }

    public PlayerController Build()
    {
        currentState = idleState;
        currentState.Enter();
        TimeDispatcher.RegisterListener(this);
        return this;
    }

    public void Tick(float deltaTime)
    {
        currentState?.Tick(deltaTime);
    }

    public PlayerController SetStartPosition(Vector3 position)
    {
        model.SetStartPosition(new Vector3(position.x, model.CurrentSize / 2f, position.z));
        model.SetPosition(model.StartPosition);
        view.SetPosition(model.Position);
        return this;
    }

    public void Reset()
    {
        model.Reset();
        view.SetSize(model.CurrentSize);
        view.SetPosition(model.Position);
        target.ResetPosition();
        chargingState.Reset();
        gameEnded = false;

        ChangeState(idleState);
    }

    private void ChangeState(IPlayerState state)
    {
        if(gameEnded)
        {
            return;
        }
        var flatDistance = target.transform.position - model.Position;
        flatDistance.y = 0f;
        if (flatDistance.magnitude < 7.5f)
        {
            gameEnded = true;
            target.OpenDoors();
            OnWin?.Invoke(Mathf.Max(VolumeCalculator.GetRemainingPercentage(model.StartSize, model.CurrentSize), 0.1f));
            return;
        }

        currentState?.Exit();
        currentState = state;
        currentState.Enter();
    }

    private void Lose()
    {
        gameEnded = true;
        currentState = null;
        chargingState.Reset();
        OnWin?.Invoke(0);
    }
}