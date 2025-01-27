using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;  
    [SerializeField] private Vector3 offset;

    private void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z) + offset;
        transform.position = desiredPosition;
    }
}
