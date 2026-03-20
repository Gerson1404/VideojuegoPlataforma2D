using UnityEngine;

public class EnemyPro : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 2f;
    public bool movingRight = true;

    [Header("Detección")]
    public Transform groundCheck;
    public Transform wallCheck;
    public float checkDistance = 0.3f;
    public LayerMask groundLayer;

    [Header("Daño")]
    public int damage = 1;
    public float bounceForce = 10f;

    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Move();
        DetectEdge();
    }

    void Update()
    {
        Animate();
    }

    void Move()
    {
        float direction = movingRight ? 1 : -1;
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    void DetectEdge()
    {
        bool groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, checkDistance, groundLayer);

        Vector2 wallDir = movingRight ? Vector2.right : Vector2.left;
        bool wallDetected = Physics2D.Raycast(wallCheck.position, wallDir, checkDistance, groundLayer);

        if (!groundDetected || wallDetected)
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (movingRight ? 1 : -1);
        transform.localScale = scale;
    }

    void Animate()
    {
        if (animator != null)
        {
            animator.SetBool("isWalking", Mathf.Abs(rb.velocity.x) > 0.1f);
        }
    }

    private float damageCooldown = 0.5f;
    private float lastDamageTime;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Time.time < lastDamageTime + damageCooldown)
                return;

            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();

            float playerY = collision.transform.position.y;
            float enemyY = transform.position.y;

            if (playerY > enemyY + 0.3f)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, bounceForce);
                Destroy(gameObject);
            }
            else
            {
                PlayerHealth player = collision.GetComponent<PlayerHealth>();
                if (player != null)
                {
                    player.TakeDamage(damage);
                    lastDamageTime = Time.time; // 🔥 evita daño continuo
                }
            }
        }
    }




    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * checkDistance);
        }

        if (wallCheck != null)
        {
            Gizmos.color = Color.red;
            Vector3 dir = movingRight ? Vector3.right : Vector3.left;
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + dir * checkDistance);
        }
    }
}
