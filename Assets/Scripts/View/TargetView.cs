using UnityEngine;
using DG.Tweening;

public class TargetView : MonoBehaviour
{
    [SerializeField] private Transform leftDoor;
    [SerializeField] private Transform rightDoor;
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float duration = 0.5f;

    private Quaternion leftInitial;
    private Quaternion rightInitial;

    private void Awake()
    {
        leftInitial = leftDoor.localRotation;
        rightInitial = rightDoor.localRotation;
    }

    public void OpenDoors()
    {
        leftDoor.DOLocalRotate(new Vector3(0f, openAngle, 0f), duration).SetEase(Ease.OutBack);
        rightDoor.DOLocalRotate(new Vector3(0f, -openAngle, 0f), duration).SetEase(Ease.OutBack);
    }

    public void ResetPosition()
    {
        leftDoor.localRotation = leftInitial;
        rightDoor.localRotation = rightInitial;
    }
}