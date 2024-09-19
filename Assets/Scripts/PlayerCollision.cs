using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public bool isDead = false;
    private AudioManager audioManager;

    // Start position of the player
    public Vector2 startPosition = new Vector2(0, 0);

    public ScrollingBackground scrollingBackground;

    private void Start()
    {
        GameManager.Instance.onPlay.AddListener(ActivatePlayer);
        GameManager.Instance.onGameOver.AddListener(HandleGameOver);
        GameManager.Instance.onResume.AddListener(ResetPlayerState); // Add listener to reset player on resume

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioManager = FindObjectOfType<AudioManager>(); // Find and store the AudioManager
    }

    public void ActivatePlayer()
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
        yield return new WaitForSeconds(0f);
        GameManager.Instance.GameOver();
    }

    public void ResetPlayerState()
    {
        rb.isKinematic = false; // Enable Rigidbody movement
        rb.velocity = Vector2.zero; // Reset velocity
        transform.position = startPosition; // Reset player position to start position
        animator.SetBool("IsAlive", true); // Set IsAlive to true
        isDead = false; // Reset death state
    }
}
