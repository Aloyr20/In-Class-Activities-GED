using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MushroomSpawn : PowerUps
{
    private GameObject mushroomPrefab;

    public MushroomSpawn(GameObject prefab)
    {
        mushroomPrefab = prefab;
    }

    public override void Spawn(Vector3 sPos)
    {
        GameObject item = GameObject.Instantiate(mushroomPrefab, sPos, Quaternion.identity);

        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        }
    }
}
