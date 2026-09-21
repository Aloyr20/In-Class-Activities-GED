using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] float speed = 12f;
    [SerializeField] float lifespan = 2f;

    Rigidbody2D rb;
    int direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = transform.right.x > 0 ? 1 : -1;
        Destroy(gameObject, lifespan);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

}