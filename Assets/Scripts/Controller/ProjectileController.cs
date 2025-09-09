using UnityEngine;

public class ProjectileController : ITickListener
{
    private ProjectileModel model;
    private ProjectileView view;
    private GameSettings settings;
    private ProjectileCollisionDetector collisionDetector;
    private ParticleSystem particleSystem;

    private bool isReleased;

    public float Size => model.Radius;

    public ProjectileController(ProjectileModel model, ProjectileView view, GameSettings settings, ParticleSystem particleSystem)
    {
        this.model = model;
        this.view = view;
        this.settings = settings;
        this.collisionDetector = view.GetComponent<ProjectileCollisionDetector>();
        this.particleSystem = particleSystem;

        collisionDetector.OnTriggered += Explode;
        view.transform.position = model.Position;

        TimeDispatcher.RegisterListener(this);
    }

    public void Release(Vector3 target)
    {
        model.Release();
        isReleased = true;
        collisionDetector.IsEnabled = true;
    }

    public void Tick(float deltaTime)
    {
        if (!isReleased) return;

        var moveStep = settings.ProjectileSpeed * deltaTime;
        var nextPos = model.Position + model.Direction * moveStep;

        model.SetPosition(nextPos);
        view.SetPosition(nextPos);
    }

    public void SetSize(float size)
    {
        model.SetRadius(size);
        view.SetSize(size);
        model.SetPosition(new Vector3(model.Position.x, size / 2f, model.Position.z));
        view.SetPosition(model.Position);
    }

    private void Explode()
    {
        ObstacleController.DeactivateInRadius(model.Position, .5f + model.Radius * settings.ExplosionMultiplier);
        Destroy();
        particleSystem.transform.position = model.Position;
        particleSystem.Emit(100);
    }

    public void Destroy()
    {
        TimeDispatcher.UnregisterListener(this);
        Object.Destroy(view.gameObject);
    }
}