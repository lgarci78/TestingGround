using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private Vector3 offset;

    private void Start()
    {
        // Calculate the initial offset between the camera and the player
        offset = transform.position - playerTransform.position;
    }

    private void LateUpdate()
    {
        // Calculate the desired camera position based on the player's position
        Vector3 desiredPosition = playerTransform.position + offset;
        transform.position = desiredPosition;
    }
}
