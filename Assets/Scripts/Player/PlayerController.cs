using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = default;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Transform feetPosition;
    [SerializeField] private float groundedDetectRadius = 0.2f;

    private bool _isGrounded;
    
    private void Start()
    {
        _isGrounded = false;
    }
    
    private void Update()
    {
        CheckIfGrounded();
        HandleInput();
    }

    private bool CheckIfGrounded()
    {
        _isGrounded = Physics.OverlapSphere(feetPosition.position, groundedDetectRadius, groundLayerMask).Length > 0;
        
        return _isGrounded;
    }

    private void HandleInput()
    {
        var horInput = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(horInput) > 0f)
        {
            Move(horInput);
        }

        if (Input.GetKeyDown(KeyCode.Space) && CheckIfGrounded())
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        _isGrounded = false;
    }

    private void Move(float moveDir)
    {
        rb.velocity = new Vector2(moveDir * speed, rb.velocity.y);
    }
}
