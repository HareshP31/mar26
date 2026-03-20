using UnityEngine;

public class SkeletonAI : MonoBehaviour
{
    public int health = 3;
    public float speed = 2f;
    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        // Move towards player
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);

        // Flip sprite
        if (direction.x < 0)
        {
            transform.localScale = new Vector3(3, 3, 3);
        }
        else if (direction.x > 0)
        {
            transform.localScale = new Vector3(-3, 3, 3);
        }

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Skeleton HP: " + health);

        // Kill skeleton
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}