using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public void SetSize(float size)
    {
        transform.localScale = Vector3.one * size;
    }

    public void ResetView(float size)
    {
        SetSize(size);
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}