using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter.Singleton;
using UnityEngine.SceneManagement;

public class MarioController : MonoBehaviour
{
    [SerializeField] float runForce;
    [SerializeField] float jumpForce;
    [SerializeField] float maxSpeed;

    [SerializeField] GameObject bigMarioPrefab;
    [SerializeField] GameObject smolMarioPrefab;
    [SerializeField] GameObject fireMarioPrefab;  // ADDED

    [SerializeField] GameObject fireballPrefab;    // ADDED
    [SerializeField] Transform firePoint;          // ADDED
    [SerializeField] float fireCooldown = 0.5f;    // ADDED

    Transform trans;
    Rigidbody2D body;

    float runInput;
    bool jumpInput;
    bool hasFirePower;          // ADDED
    float lastFireTime;         // ADDED

    bool isGrounded;
    bool isBig;
    bool isDead;
    bool isRunning;

    bool deathStarted;

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
        body = GetComponent<Rigidbody2D>();

        AudioManager.Instance.PlayMusic("Music");
    }

    // Update is called once per frame
    void Update()
    {
        runInput = Input.GetAxis("Horizontal");

        if (runInput == 0)
        {
            isRunning = false;
        }

        // ADDED FIRE INPUT
        if (Input.GetKeyDown(KeyCode.Space) && hasFirePower && Time.time > lastFireTime + fireCooldown)
        {
            ShootFireball();
            lastFireTime = Time.time;
        }

        if (Input.GetKey(KeyCode.W))
        {
            jumpInput = true;
        }
        else
        {
            jumpInput = false;
        }

        if (runInput == 0 && body.linearVelocity.y == 0)
        {
            body.linearDamping = 3;
        }
        else
        {
            body.linearDamping = 1;
        }

        if (trans.position.y <= -5 && !deathStarted)
        {
            StartDeath();
        }

        if (trans.position.y <= -7)
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        if (runInput != 0)
        {
            Run();
        }

        if (jumpInput && isGrounded)
        {
            Jump();
        }
    }

    void Run()
    {
        isRunning = true;

        if (Mathf.Abs(body.linearVelocity.x) >= maxSpeed)
        {
            return;
        }

        if (runInput > 0)
        {
            body.AddForce(Vector2.right * runForce, ForceMode2D.Force);
            trans.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (runInput < 0)
        {
            body.AddForce(Vector2.left * runForce, ForceMode2D.Force);
            trans.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void Jump()
    {
        body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;

        AudioManager.Instance.Play("Jump");
    }

    public void EnableFirePower()
    {
        hasFirePower = true;
        GetComponent<BoxCollider2D>().size = fireMarioPrefab.GetComponent<BoxCollider2D>().size;
    }

    void ShootFireball()
    {
        if (fireballPrefab == null || firePoint == null) return;

        float direction = trans.rotation.eulerAngles.y == 180 ? 1 : -1;

        Vector3 spawnPos = firePoint.position + new Vector3(direction * 0.5f, 0, 0);

        GameObject fireball = Instantiate(fireballPrefab, spawnPos, Quaternion.identity);

        fireball.transform.right = new Vector2(direction, 0);
    }

    void EnemyBounce()
    {
        body.AddForce(Vector2.up * jumpForce / 1.5f,ForceMode2D.Impulse);
        isGrounded = false;
    }

    void StartDeath()
    {
        FindObjectOfType<Lives>().LoseLife();

        AudioManager.Instance.StopMusic();

        if (FindObjectOfType<Lives>().GetCurrentLives() < 1)
        {
            AudioManager.Instance.Play("GameOver");
        }
        else
        {
            AudioManager.Instance.PlayMusic("LifeLost");
            ScoreCounter.Instance.ResetScore();
        }

        isDead = true;

        body.linearVelocity = Vector2.zero;

        body.gravityScale = 3;

        body.AddForce(Vector3.up * jumpForce / 2,ForceMode2D.Impulse);

        GetComponent<Collider2D>().enabled = false;

        deathStarted = true;
    }

    void Die()
    {
        if (FindObjectOfType<Lives>().GetCurrentLives() < 1)
        {
            FindObjectOfType<LevelStatus>().SetGameOver(true);
        }
        else
        {
            FindObjectOfType<LevelStatus>().SetLevelFailed(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        for (int i = 0; i < collision.contacts.Length; i++)
        {
            if (collision.contacts[i].normal.y > 0.5)
            {
                isGrounded = true;
            }
        }

        if (collision.gameObject.tag == "Goomba")
        {
            Goomba goomba = collision.gameObject.GetComponent<Goomba>();
             
            if (collision.contacts[0].normal.y > 0.5)
            {
                EnemyBounce();

                goomba.Squash();

                ScoreCounter.Instance.AddScore(1000);

                AudioManager.Instance.Play("Bump");
            }
            else
            {
                if (!goomba.IsSquashed)
                {
                    if (isBig)
                    {
                        hasFirePower = false;

                        GetComponent<BoxCollider2D>().size = smolMarioPrefab.GetComponent<BoxCollider2D>().size;

                        isBig = false;

                        AudioManager.Instance.Play("PowerDown");
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                        StartDeath();
                    }
                }
            }
        }

        if (collision.gameObject.tag == "Koopa")
        {
            Koopa koopa = collision.gameObject.GetComponent<Koopa>();

            if (collision.contacts[0].normal.y > 0.5 && !koopa.IsSquashed)
            {
                EnemyBounce();

                koopa.Squash();

                collision.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);

                ScoreCounter.Instance.AddScore(1000);

                AudioManager.Instance.Play("Bump");
            }
            else if (koopa.IsSquashed && !koopa.IsKicked)
            {
                if (collision.gameObject.transform.position.x > trans.position.x)
                {
                    koopa.ApplyKickForce(new Vector2(1, 0));
                }

                if (collision.gameObject.transform.position.x < trans.position.x)
                {
                    koopa.ApplyKickForce(new Vector2(-1, 0));
                }
            }
            else if (collision.contacts[0].normal.y > 0.5 && koopa.IsKicked)
            {
                EnemyBounce();

                koopa.StopKick();

                collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0,collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity.y);

                ScoreCounter.Instance.AddScore(100);
            }
            else
            {
                if (!koopa.IsMoving)
                {
                    if (isBig)
                    {
                        hasFirePower = false;

                        GetComponent<BoxCollider2D>().size = smolMarioPrefab.GetComponent<BoxCollider2D>().size;

                        isBig = false;

                        AudioManager.Instance.Play("PowerDown");
                    }
                    else
                    {
                        StartDeath();
                    }
                }
            }
        }

        if (collision.gameObject.name.Contains("Mushroom"))
        {
            AudioManager.Instance.Play("PowerUp");

            if (!isBig)
            {
                Destroy(collision.gameObject);

                isBig = true;

                GetComponent<BoxCollider2D>().size = bigMarioPrefab.GetComponent<BoxCollider2D>().size;
            }
            else
            {
                Destroy(collision.gameObject);

                ScoreCounter.Instance.AddScore(500);
            }
        }

        if (collision.gameObject.name.Contains("FireFlower"))
        {
            Destroy(collision.gameObject);
            hasFirePower = true;

            GetComponent<BoxCollider2D>().size = fireMarioPrefab.GetComponent<BoxCollider2D>().size;

            isBig = true;
        }

        if (collision.gameObject.CompareTag("FireballP"))
        {
            if (isBig)
            {
                hasFirePower = false;

                GetComponent<BoxCollider2D>().size = smolMarioPrefab.GetComponent<BoxCollider2D>().size;

                isBig = false;
            }
            else
            {
                StartDeath();
            }
        }
    }

    public bool GetIsRunning()
    {
        return isRunning;
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    public bool GetIsDead()
    {
        return isDead;
    }

    public bool GetIsBig()
    {
        return isBig;
    }
}
