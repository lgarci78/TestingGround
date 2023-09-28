using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float offset;

    void Update() {
        transform.position = player.transform.position + new Vector3(0, 0, -offset);
    }
}
