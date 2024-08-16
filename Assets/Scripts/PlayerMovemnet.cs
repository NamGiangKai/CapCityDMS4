using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 10f; // The force applied when the character jumps
    public LayerMask groundLayer; // Layer mask to identify the ground
    public Transform groundCheck; // Transform to check if the character is on the ground
    public float groundCheckRadius = 1f; // Radius of the ground check circle

    private Rigidbody2D rb;
    private Animator animator;
    private AudioManager audioManager; // Reference to the AudioManager
    private bool isGrounded;
    private bool isRunningSoundPlaying;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>(); // Find the AudioManager in the scene
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

        // Play or stop the run sound based on whether the character is running on the ground
        if (isGrounded && Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            if (!isRunningSoundPlaying)
            {
                audioManager.PlayRunSound(); // Play the run sound
                isRunningSoundPlaying = true;
            }
        }
        else
        {
            if (isRunningSoundPlaying)
            {
                audioManager.StopRunSound(); // Stop the run sound
                isRunningSoundPlaying = false;
            }
        }
    }

    void Jump()
    {
        // Apply vertical force to make the character jump
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        // Trigger the jump animation
        animator.SetTrigger("Jump");

        // Stop the running sound if it's playing
        if (isRunningSoundPlaying)
        {
            audioManager.StopRunSound();
            isRunningSoundPlaying = false;
        }

        // Play the jump sound
        audioManager.PlayJumpSound(); // Ensure the AudioManager has the jump sound correctly assigned
    }

    public void Die()
    {
        // Play the death sound and stop the run sound
        audioManager.PlayDeathSound();
        // Handle the rest of the death logic here...
    }

    void OnDrawGizmos()
    {
        // Draw a gizmo to show the ground check circle in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
