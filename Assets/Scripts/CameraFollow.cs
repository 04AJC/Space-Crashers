using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The player to follow
    public float smoothSpeed = 0.125f; // How smooth the camera follows
    public Vector3 offset; // Distance from the player

    void LateUpdate()
    {
        // Only follow X axis, lock Y at a fixed value
        Vector3 desiredPosition = new Vector3(target.position.x, transform.position.y, target.position.z) + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}