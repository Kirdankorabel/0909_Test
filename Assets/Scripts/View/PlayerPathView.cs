using UnityEngine;

public class PlayerPathView : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    public void SetWidth(float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }
}