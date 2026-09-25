using Chapter.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    [SerializeField] GameObject blockItem;
    [SerializeField] PowerUpsSpawner powerUpsSpawner;
    [SerializeField] Sprite usedBlock;
    [SerializeField] Sprite unusedBlock;

    SpriteRenderer spriteRenderer;
    bool blockHit = false;
    bool blockHitActionPerformed = false;
    GameObject item = null;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!blockHit)
        {
            spriteRenderer.sprite = unusedBlock;
        }
        else
        {
            spriteRenderer.sprite = usedBlock;

            if (!blockHitActionPerformed)
            {
                BlockHitAction();
            }
        }
    }

    void BlockHitAction()
    {
        if (blockItem.CompareTag("Coin"))
        {
            item = Instantiate(blockItem, transform.position + new Vector3(0, 1, 0), Quaternion.identity);

            Destroy(item, 0.5f);

            item.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5, ForceMode2D.Impulse);

            FindObjectOfType<CoinCounter>().AddCoin(1);
            ScoreCounter.Instance.AddScore(100);

            FindObjectOfType<AudioManager>().Play("Coin");
        }
        else if (blockItem.CompareTag("Powerup"))
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, 1.01f, 0);

            PowerUps powerUp = powerUpsSpawner.SpawnPowerUp();

            powerUp.Spawn(spawnPosition);

            FindObjectOfType<AudioManager>().Play("MushroomSpawn");
        }

        blockHitActionPerformed = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.contacts[0].normal.y > 0.5f)
            {
                blockHit = true;
                FindObjectOfType<AudioManager>().Play("Bump");
            }
        }
    }
}