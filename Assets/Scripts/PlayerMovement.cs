using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    private Vector2 moveDirection;
    private Vector2 lookDirection;

    // Update is called once per frame
    void Update()
    {
        ProccessInput();
        LookRotation();
    }

    void FixedUpdate() {
        Move();
    }

    void ProccessInput() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(horizontal, vertical).normalized;
    }

    void Move() {
        rb.velocity = new Vector2(moveDirection.x  * moveSpeed, moveDirection.y * moveSpeed);
    }

    void LookRotation() {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
