using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Koopa : Enemy
{
    [SerializeField] private float kickForce = 2.5f;

    private bool isKicked;
    private bool isMoving = true;

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
        base.Squash();
        isMoving = false;
    }

    public void ApplyKickForce(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(direction * kickForce,ForceMode2D.Impulse);

        isKicked = true;
        isMoving = false;

        FindObjectOfType<AudioManager>().Play("Kick");
    }

    public void StopKick()
    {
        isKicked = false;
        isMoving = false;
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

                Destroy(collision.gameObject, 2);

                FindObjectOfType<AudioManager>().Play("Bump");

                ApplyKickForce(new Vector2(-collision.contacts[0].normal.normalized.x,0));
            }
        }
    }
}