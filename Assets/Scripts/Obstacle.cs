using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Notify the GameManager that the player passed the obstacle
            GameManager.Instance.PlayerJumpedOverObstacle();

            
        }
    }
}
