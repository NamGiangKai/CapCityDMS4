using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value;
    private Animator animator;
    private AudioManager audioManager;
    private bool isCollected = false; // To prevent multiple triggers

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component attached to the coin
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isCollected)
        {
            isCollected = true; // Set the flag to prevent multiple triggers
            CoinCounter.instance.IncreaseCoins(value);
            PlayDisappearAnimation(); // Play the coin disappear animation
            audioManager.PlayCoinSound(); 
        }
    }

    void PlayDisappearAnimation()
    {
        if (animator != null)
        {
            Debug.Log("Triggering Disappear animation");
            animator.SetTrigger("Disappear"); // Trigger the disappear animation

            // Wait for the animation to complete before destroying the coin
            StartCoroutine(DestroyAfterAnimation());
        }
        else
        {
            Debug.LogWarning("Animator not found!");
            Destroy(gameObject); // If no animator is found, destroy immediately
        }
    }


    private IEnumerator DestroyAfterAnimation()
    {
        // Wait until the animation completes (assuming the animation length is 1 second)
        yield return new WaitForSeconds(1f);

        Destroy(gameObject); // Destroy the coin after the animation completes
    }
}
