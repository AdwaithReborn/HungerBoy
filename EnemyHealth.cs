using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;

    private float currentHealth;

    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D enemyCollider;
    private EnemyAI enemyAI;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();
        enemyAI = GetComponent<EnemyAI>();
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;

        Debug.Log("Enemy Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("Hurt");
        }
    }

    void Die()
    {
        if (isDead)
            return;

        isDead = true;

        // Stop enemy AI
        if (enemyAI != null)
            enemyAI.enabled = false;

        // Stop physics movement
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        // Disable collision
        if (enemyCollider != null)
            enemyCollider.enabled = false;

        // Play death animation
        animator.SetTrigger("Die");

        // Destroy after death animation
        Destroy(gameObject, 1f);
    }
}