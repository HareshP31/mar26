using UnityEngine;

public class KnightController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 8f;
    public float jumpForce = 4f;

    [Header("Detection")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    private bool facingRight = true;
    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Horizontal Movement Input (A/D)
        moveInput = Input.GetAxisRaw("Horizontal");

        // Update Animator Speed
        anim.SetFloat("Speed", Mathf.Abs(moveInput));

        // Jumping Input (Space)
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Attack Input (Left Click)
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
            HandleAttack();
        }

        // Flip Sprite Logic
        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        // Apply the actual movement
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void HandleAttack()
    {
        // Offset the circle so it's in front of the knight, not at his center
        Vector2 attackPoint = transform.position + (transform.right * 0.2f);
        float attackRange = 1.5f;
        LayerMask enemyLayer = LayerMask.GetMask("Enemy");

        // Look for enemy within a circle
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Output to console
            Debug.Log("Hit something: " + enemy.name);
            SkeletonAI skeleton = enemy.GetComponent<SkeletonAI>();
            if (skeleton != null)
            {
                skeleton.TakeDamage(1);
            }
        }
    }

    // Helps you see the ground check in the editor
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        // Gizmos.color = Color.red;
        // how far
        // Vector3 attackPoint = transform.position + (transform.right * 0.8f);
        // circle size
        // Gizmos.DrawWireSphere(attackPoint, 1.2f);
    }
}
