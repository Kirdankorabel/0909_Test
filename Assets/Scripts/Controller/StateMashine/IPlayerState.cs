public interface IPlayerState
{
    event System.Action<IPlayerState> OnStateRequest;
    void Initialize(PlayerModel model, PlayerView view);
    void Enter();
    void Tick(float deltaTime);
    void Exit();
}