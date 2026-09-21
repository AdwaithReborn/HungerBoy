using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5f;

    private float cameraOffsetX;
    private float startingCameraX;
    private float fixedY;

    void Start()
    {
        // Remember the camera's starting position
        startingCameraX = transform.position.x;
        fixedY = transform.position.y;

        // Remember where the player is relative to the camera
        cameraOffsetX = transform.position.x - player.position.x;
    }

    void LateUpdate()
    {
        if (player == null)
            return;

        // Follow player while keeping the original screen position
        float targetX = player.position.x + cameraOffsetX;

        // Don't allow camera to move further left than its starting position
        targetX = Mathf.Max(targetX, startingCameraX);

        Vector3 targetPosition = new Vector3(
            targetX,
            fixedY,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}