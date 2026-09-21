using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 25f;

    private int direction;

    void Start()
    {
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        transform.position +=
            Vector3.right * direction * speed * Time.deltaTime;
    }

    public void SetDirection(int dir)
    {
        direction = dir;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}