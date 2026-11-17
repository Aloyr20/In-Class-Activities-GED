using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private int maxPoolSize = 64;

    private List<Enemy> enemies = new List<Enemy>();

    public void CreatePool(Enemy enemyPrefab)
    {
        if (enemies.Count > 0)
        {
            return;
        }

        for (int i = 0; i < maxPoolSize; i++)
        {
            Enemy enemy = Instantiate(enemyPrefab, transform);
            enemy.SetPool(this);
            enemy.gameObject.SetActive(false);
            enemies.Add(enemy);
        }
    }

    public Enemy GetEnemy(Vector3 position, float lifetime)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].gameObject.activeSelf)
            {
                enemies[i].transform.position = position;
                enemies[i].transform.rotation = Quaternion.identity;
                enemies[i].gameObject.SetActive(true);
                enemies[i].SetLifetime(lifetime);
                return enemies[i];
            }
        }

        return null;
    }

    public void ReturnToPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }
}