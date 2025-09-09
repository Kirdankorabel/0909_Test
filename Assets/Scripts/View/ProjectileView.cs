using UnityEngine;

public class ProjectileView : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionParticle;

    public void SetSize(float size)
    {
        transform.localScale = Vector3.one * size;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}