using UnityEngine;

public class HungerBoyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;

    // Dash-slide settings
    public float slideSpeed = 8f;
    public float slideDuration = 0.25f;

    public GameObject firePrefab;
    public Transform firePoint;

    private Rigidbody2D rb;
    private Animator animator;

    private bool isGrounded;
    private bool isSliding;
    private float slideTimer;

    private int facingDirection = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleAttack();
        HandleDashSlide();
    }

    void HandleMovement()
    {
        // Don't allow normal movement while sliding
        if (isSliding)
            return;

        float move = 0f;

        // A = backward / left
        if (Input.GetKey(KeyCode.A))
        {
            move = -1f;
        }

        // D = forward / right
        if (Input.GetKey(KeyCode.D))
        {
            move = 1f;
        }

        rb.linearVelocity = new Vector2(
            move * moveSpeed,
            rb.linearVelocity.y
        );

        // Running animation
        animator.SetBool("IsRunning", move != 0);

        // Character direction
        if (move != 0)
        {
            facingDirection = move > 0 ? 1 : -1;

            transform.localScale = new Vector3(
                facingDirection,
                1,
                1
            );
        }
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isSliding)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );

            animator.SetTrigger("Jump");

            isGrounded = false;
        }
    }

    void HandleAttack()
{
    if (Input.GetKeyDown(KeyCode.RightControl) && !isSliding)
    {
        animator.SetTrigger("Attack");

        ShootFire();
    }
}

    void HandleDashSlide()
    {
        // SHIFT = DASH SLIDE
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && !isSliding)
       {
             StartSlide();
        }     

        if (isSliding)
        {
            slideTimer -= Time.deltaTime;

            rb.linearVelocity = new Vector2(
                facingDirection * slideSpeed,
                rb.linearVelocity.y
            );

            if (slideTimer <= 0)
            {
                StopSlide();
            }
        }
    }

    void StartSlide()
    {
        isSliding = true;
        slideTimer = slideDuration;

        animator.SetTrigger("DashSlide");

        // Make sure Hunger Boy moves immediately
        rb.linearVelocity = new Vector2(
            facingDirection * slideSpeed,
            rb.linearVelocity.y
        );
    }

    void StopSlide()
    {
        isSliding = false;

        rb.linearVelocity = new Vector2(
            0,
            rb.linearVelocity.y
        );
    }

    void EatFood(GameObject food)
{
    if (isSliding)
        return;

    animator.SetTrigger("Eat");

    HungerBoyHealth health =
        GetComponent<HungerBoyHealth>();

    if (health != null)
        health.Heal(20f);

    StartCoroutine(RemoveFoodAfterEat(food));
}

    void ShootFire()
{
    if (firePrefab == null)
    {
        Debug.LogError("Fire Prefab is NOT assigned!");
        return;
    }

    if (firePoint == null)
    {
        Debug.LogError("Fire Point is NOT assigned!");
        return;
    }

    GameObject fire = Instantiate(
        firePrefab,
        firePoint.position,
        Quaternion.identity
    );

    FireProjectile projectile = fire.GetComponent<FireProjectile>();

    if (projectile == null)
    {
        Debug.LogError("Fire prefab does not have FireProjectile script!");
        return;
    }

    projectile.SetDirection(facingDirection);
}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    System.Collections.IEnumerator RemoveFoodAfterEat(GameObject food)
{
    yield return new WaitForSeconds(0.3f);

    if (food != null)
        Destroy(food);
}

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Food"))
    {
        EatFood(other.gameObject);
    }
}
}