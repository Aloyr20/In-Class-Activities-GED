using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;

    private bool isSquashed;
    private bool movingLeft;
    private EnemyPool enemyPool;
    private float lifetime;
    private Rigidbody2D body;
    private BoxCollider2D enemyCollider;
    private Vector2 colliderSize;
    private float gravityScale;

    public bool IsSquashed
    {
        get
        {
            return isSquashed;
        }

        protected set
        {
            isSquashed = value;
        }
    }

    protected float Speed
    {
        get
        {
            return speed;
        }
    }

    protected bool MovingLeft
    {
        get
        {
            return movingLeft;
        }

        set
        {
            movingLeft = value;
        }
    }

    protected virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<BoxCollider2D>();
        colliderSize = enemyCollider.size;
        gravityScale = body.gravityScale;
    }

    protected virtual void OnEnable()
    {
        IsSquashed = false;
        MovingLeft = false;
        lifetime = 0;
        body.linearVelocity = Vector2.zero;
        body.angularVelocity = 0;
        body.gravityScale = gravityScale;
        enemyCollider.enabled = true;
        enemyCollider.size = colliderSize;
    }

    public void SetPool(EnemyPool pool)
    {
        enemyPool = pool;
    }

    public void SetLifetime(float time)
    {
        lifetime = time;
    }

    public void Despawn()
    {
        UnityEngine.Profiling.Profiler.BeginSample("Enemy.Despawn");

        if (enemyPool != null)
        {
            enemyPool.ReturnToPool(this);
        }
        else
        {
            Destroy(gameObject);
        }

        UnityEngine.Profiling.Profiler.EndSample();
    }

    protected virtual void Update()
    {
        if (lifetime > 0)
        {
            lifetime -= Time.deltaTime;

            if (lifetime <= 0)
            {
                Despawn();
                return;
            }
        }

        if (!IsSquashed)
        {
            Move();
        }
    }

    protected virtual void Move()
    {
        if (MovingLeft)
        {
            transform.position += Vector3.left * Time.deltaTime * Speed;
        }
        else
        {
            transform.position += Vector3.right * Time.deltaTime * Speed;
        }
    }

    public virtual void Squash()
    {
        IsSquashed = true;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fireball"))
        {
            Destroy(collision.gameObject);
            Despawn();
        }
    }
}