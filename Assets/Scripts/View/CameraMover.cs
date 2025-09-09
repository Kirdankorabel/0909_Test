using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Vector3 offset;

    private void Start()
    {
        Vector3 flatTargetPos = new Vector3(target.position.x, 0f, target.position.z);
        Vector3 flatCamPos = new Vector3(transform.position.x, 0f, transform.position.z);
        offset = flatCamPos - flatTargetPos;
    }

    private void LateUpdate()
    {
        Vector3 newPos = new Vector3(
            target.position.x + offset.x,
            transform.position.y,
            target.position.z + offset.z
        );
        transform.position = newPos;
    }
}