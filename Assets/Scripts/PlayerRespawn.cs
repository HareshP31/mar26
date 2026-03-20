using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    // Drag checkpoint in inspector
    public Transform currentCheckpoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hazard"))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        // Move the player to the checkpoint 
        transform.position = currentCheckpoint.position;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        // Output to console
        Debug.Log("Player hit a hazard! Respawning...");
    }
}