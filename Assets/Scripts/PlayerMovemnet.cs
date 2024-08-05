using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 10f; // The force applied when the character jumps
    public LayerMask groundLayer; // Layer mask to identify the ground
    public Transform groundCheck; // Transform to check if the character is on the ground
    public float groundCheckRadius = 0.2f; // Radius of the ground check circle

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the character is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Jump when the screen is touched and the character is on the ground
        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            Jump();
        }

        // Set the running animation when the character is on the ground and not jumping
        animator.SetBool("isGrounded", isGrounded);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        animator.SetTrigger("Jump");
    }

    void OnDrawGizmos()
    {
        // Draw a gizmo to show the ground check circle in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
