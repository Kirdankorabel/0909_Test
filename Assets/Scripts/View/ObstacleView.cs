using UnityEngine;
using DG.Tweening;

public class ObstacleView : MonoBehaviour
{
    [Header("Renderer and Colors")]
    [SerializeField] private Renderer objectRenderer;
    [SerializeField] private Color infectedColor = Color.red; 

    private bool infected;

    public bool IsAlive { get; private set; }

    private Color originalColor;

    private void Awake()
    {
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
    }

    public void Infect()
    {
        if (infected) return;
        infected = true;

        if (objectRenderer != null)
        {
            objectRenderer.material.color = infectedColor;
        }

        transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
        {
            IsAlive = false;
            SetInactive();
            if (objectRenderer != null)
            {
                objectRenderer.material.color = originalColor;
            }
        });
    }

    public void SetActive()
    {
        infected = false;
        transform.localScale = Vector3.one;
        gameObject.SetActive(true);
        IsAlive = true;

        if (objectRenderer != null)
        {
            objectRenderer.material.color = originalColor;
        }
    }

    public void SetInactive()
    {
        gameObject.SetActive(false);
        IsAlive = false;
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }
}