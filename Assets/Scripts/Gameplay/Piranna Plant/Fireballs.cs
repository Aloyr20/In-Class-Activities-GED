using UnityEngine;

public class Fireballs : MonoBehaviour
{
    public Transform player;
    public float speed = 10f;
    [Tooltip("How quickly the fireball turns toward the player (degrees/sec). 0 = instant tracking.")]
    public float turnSpeed = 200f;
    private Rigidbody2D rb2d;

    void Start()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        rb2d = GetComponent<Rigidbody2D>();

        // Set initial direction toward the player
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            if (rb2d != null)
                rb2d.linearVelocity = direction * speed;
        }
    }

    void Update()
    {
        if (player == null)
            return;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player has been hit by the fireball.");
            // Disable collider to stop further physical interactions.
            Collider2D col = GetComponent<Collider2D>();
            if (col != null)
            {
                col.enabled = false;
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
