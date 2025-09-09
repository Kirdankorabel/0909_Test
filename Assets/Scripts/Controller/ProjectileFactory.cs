using UnityEngine;

public class ProjectileFactory
{
    private GameSettings settings;
    private ProjectileView prefab;
    private ParticleSystem particleSystem;

    public ProjectileFactory(GameSettings settings, ProjectileView prefab, ParticleSystem particleSystem)
    {
        this.settings = settings;
        this.prefab = prefab;
        this.particleSystem = particleSystem;
    }

    public ProjectileController Create(Vector3 playerPosition, Vector3 targetPosition, float initialRadius = 0f)
    {
        var direction = targetPosition - playerPosition;
        direction.y = 0f;
        direction.Normalize();

        var spawnPos = playerPosition + direction * settings.StartSize;

        var instance = Object.Instantiate(prefab, spawnPos, Quaternion.identity);
        var model = new ProjectileModel(spawnPos, targetPosition, initialRadius);
        var controller = new ProjectileController(model, instance, settings, particleSystem);

        return controller;
    }
}