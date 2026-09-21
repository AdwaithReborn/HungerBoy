using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float attackRange = 1.2f;

    public float attackCooldown = 1.5f;
    public float attackDamage = 10f;

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;

    private float attackTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Hunger Boy must have the Player tag!");
        }
    }

    void Update()
    {
        if (player == null)
            return;

        attackTimer -= Time.deltaTime;

        float distance = Vector2.Distance(
            transform.position,
            player.position
        );

        // Attack when close
        if (distance <= attackRange)
        {
            StopMoving();
            AttackPlayer();
        }
        else
        {
            // ALWAYS chase player
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        float direction = Mathf.Sign(
            player.position.x - transform.position.x
        );

        rb.linearVelocity = new Vector2(
            direction * moveSpeed,
            rb.linearVelocity.y
        );

        // Fiend's original artwork faces LEFT
        if (direction < 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        animator.Play("Fiend_Run");
    }

    void StopMoving()
    {
        rb.linearVelocity = new Vector2(
            0f,
            rb.linearVelocity.y
        );
    }

    void AttackPlayer()
    {
        if (attackTimer > 0)
            return;

        attackTimer = attackCooldown;

        animator.SetTrigger("Attack");

        HungerBoyHealth health =
            player.GetComponent<HungerBoyHealth>();

        if (health != null)
        {
            health.TakeDamage(attackDamage);
        }
    }
}