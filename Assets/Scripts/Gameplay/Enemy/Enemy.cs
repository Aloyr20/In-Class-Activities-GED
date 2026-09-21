using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;

    private bool isSquashed;
    private bool movingLeft;

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

    protected virtual void Update()
    {
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
            Destroy(gameObject);
        }
    }
}