using UnityEngine;

public class Goomba : Enemy
{
    [SerializeField] private float deathTimer = 0.2f;

    private float flipTimer;

    protected override void OnEnable()
    {
        base.OnEnable();
        flipTimer = 0;
        GetComponent<Animator>().SetBool("Squashed", false);
    }

    protected override void Update()
    {
        base.Update();

        if(!gameObject.activeSelf)
        {
            return;
        }

        if (flipTimer <= Time.realtimeSinceStartup)
        {
            transform.Rotate(new Vector3(0, 1, 0), 180);
            flipTimer = Time.realtimeSinceStartup + 0.25f;
        }
    }

    public override void Squash()
    {
        if(IsSquashed)
        {
            return;
        }

        base.Squash();
        GetComponent<Animator>().SetBool("Squashed", true);
        SetLifetime(deathTimer);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (!collision.gameObject.CompareTag("Fireball"))
        {
            MovingLeft = !MovingLeft;
        }
    }
}