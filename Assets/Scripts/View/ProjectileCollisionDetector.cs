using UnityEngine;

public class ProjectileCollisionDetector : MonoBehaviour
{
    public event System.Action OnTriggered;

    public bool IsEnabled { get; set; } = false;

    private void OnTriggerEnter(Collider other)
    {
        if (IsEnabled && (other.gameObject.GetComponent<ObstacleView>() || other.gameObject.GetComponent<TargetView>()))
        {
            OnTriggered?.Invoke();
        }
    }
}