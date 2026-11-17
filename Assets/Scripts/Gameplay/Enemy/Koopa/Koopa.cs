using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Koopa : Enemy
{
    [SerializeField] private float kickForce = 2.5f;

    private bool isKicked;
    private bool isMoving = true;
    private Animator koopaAnimator;

    public bool IsKicked
    {
        get 
        { 
            return isKicked; 
        }

    }

    public bool IsMoving
    {
        get
        {
            return isMoving;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        koopaAnimator = GetComponent<Animator>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        isKicked = false;
        isMoving = true;
        koopaAnimator.SetBool("isSquashed", false);
        koopaAnimator.SetBool("isKicked", false);
        koopaAnimator.Play("Walk", 0, 0);
    }

    protected override void Move()
    {
        if (MovingLeft)
        {
            transform.position += Vector3.left * Time.deltaTime * Speed;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.position += Vector3.right * Time.deltaTime * Speed;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        isMoving = true;
    }

    public override void Squash()
    {
        if (IsSquashed)
        {
            return;
        }

        base.Squash();
        isMoving = false;
        koopaAnimator.SetBool("isSquashed", true);
    }

    public void ApplyKickForce(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(direction * kickForce,ForceMode2D.Impulse);

        isKicked = true;
        isMoving = false;
        koopaAnimator.SetBool("isKicked", true);

        AudioManager.Instance.Play("Kick");
    }

    public void StopKick()
    {
        isKicked = false;
        isMoving = false;
        koopaAnimator.SetBool("isKicked", false);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (collision.gameObject.CompareTag("Fireball"))
        {
            return;
        }

        if (!IsSquashed)
        {
            MovingLeft = !MovingLeft;
        }

        if (isKicked)
        {
            if (collision.contacts[0].normal.x > 0)
            {
                ApplyKickForce(Vector2.right);
            }

            if (collision.contacts[0].normal.x < 0)
            {
                ApplyKickForce(Vector2.left);
            }

            if (collision.gameObject.CompareTag("Goomba"))
            {
                Rigidbody2D goomba = collision.gameObject.GetComponent<Rigidbody2D>();

                goomba.gravityScale = 3;

                goomba.AddForce(Vector2.up * 8,ForceMode2D.Impulse);

                collision.gameObject.GetComponent<Collider2D>().enabled = false;

                collision.gameObject.GetComponent<Goomba>().SetLifetime(2);

                AudioManager.Instance.Play("Bump");

                ApplyKickForce(new Vector2(-collision.contacts[0].normal.normalized.x,0));
            }
        }
    }
}