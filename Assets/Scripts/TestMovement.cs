using UnityEngine;

public class TestMovement : MonoBehaviour
{
    public float speed = 4f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            Debug.Log("Test velocity set to: " + rb.velocity);
        }
        else
        {
            Debug.LogError("Rigidbody2D component not found.");
        }
    }
}
