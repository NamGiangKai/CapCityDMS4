using System.Collections; // This is the missing namespace
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private bool isDead = false;

    private void Start()
    {
        GameManager.Instance.onPlay.AddListener(ActivatePlayer);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void ActivatePlayer()
    {
        gameObject.SetActive(true);
        rb.velocity = Vector2.zero;
        rb.isKinematic = false;
        isDead = false;
        animator.SetBool("IsAlive", true); // Set IsAlive to true when the game starts
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        if (collision.transform.CompareTag("obstacle"))
        {
            // Stop the player's movement
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;

            // Stop the obstacle's movement
            Rigidbody2D obstacleRb = collision.collider.GetComponent<Rigidbody2D>();
            if (obstacleRb != null)
            {
                obstacleRb.velocity = Vector2.zero;
                obstacleRb.isKinematic = true;
            }

            // Play the death animation
            animator.SetTrigger("Death");
            animator.SetBool("IsAlive", false); // Set IsAlive to false when the player dies

            // Call GameOver after a short delay
            StartCoroutine(GameOverAfterDelay());
        }
    }

    private IEnumerator GameOverAfterDelay()
    {
        isDead = true;
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.GameOver();
    }
}
