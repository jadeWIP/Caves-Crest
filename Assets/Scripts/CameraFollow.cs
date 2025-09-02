using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 0f, -10f); // The offset is used to set the initial position of the camera relative to the target.
    private float smoothTime = 0.25f; // smoothTime determines how quickly the camera follows the target.
    private Vector3 velocity = Vector3.zero; // velocity is used by Vector3.SmoothDamp to smooth the camera movement.

    [SerializeField] private Transform target; // The target is the object that the camera will follow.

    private void Update()
    {
        // Calculate the target position by adding the offset to the target's current position.
        Vector3 targetPosition = target.position + offset;

        // Use Vector3.SmoothDamp to smoothly interpolate between the current camera position,
        // the target position, and the velocity over time (smoothTime).
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
