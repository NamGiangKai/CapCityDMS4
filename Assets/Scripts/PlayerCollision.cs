using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private bool isDead = false;
    private AudioManager audioManager;

    // Start position of the player
    public Vector2 startPosition = new Vector2(0, 0);

    private void Start()
    {
        GameManager.Instance.onPlay.AddListener(ActivatePlayer);
        GameManager.Instance.onGameOver.AddListener(HandleGameOver);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioManager = FindObjectOfType<AudioManager>(); // Find and store the AudioManager
    }

    private void ActivatePlayer()
    {
        gameObject.SetActive(true);
        transform.position = startPosition; // Reset player position
        rb.velocity = Vector2.zero;
        rb.isKinematic = false;
        isDead = false;
        animator.SetBool("IsAlive", true); // Set IsAlive to true when the game starts
    }

    private void HandleGameOver()
    {
        // Optionally, you could reset player state here if needed
        // ResetPlayerState();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        if (collision.transform.CompareTag("obstacle"))
        {
            // Stop the player's movement
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;

            // Play the death animation
            animator.SetTrigger("Death");
            animator.SetBool("IsAlive", false); // Set IsAlive to false when the player dies

            // Play the death sound
            if (audioManager != null && audioManager.death != null)
            {
                audioManager.PlaySFX(audioManager.death);
            }

            // Set isDead flag to true
            isDead = true;

            // Call GameOver after a short delay
            StartCoroutine(GameOverAfterDelay());
        }
    }

    private IEnumerator GameOverAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.GameOver();
    }

    private void ResetPlayerState()
    {
        rb.isKinematic = false;
        rb.velocity = Vector2.zero;
        animator.SetBool("IsAlive", true);
        isDead = false;
    }
}
