using UnityEngine;

public class Goomba : Enemy
{
    [SerializeField] private float deathTimer = 0.2f;

    private float flipTimer;

    protected override void Update()
    {
        base.Update();

        if (IsSquashed)
        {
            Destroy(gameObject, deathTimer);
        }

        if (flipTimer <= Time.realtimeSinceStartup)
        {
            transform.Rotate(new Vector3(0, 1, 0), 180);
            flipTimer = Time.realtimeSinceStartup + 0.25f;
        }
    }

    public override void Squash()
    {
        base.Squash();
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