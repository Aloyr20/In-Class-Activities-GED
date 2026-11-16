using UnityEngine;
using UnityEngine.Profiling;

public class GoombaSpawner : MonoBehaviour
{
    [SerializeField] private Goomba enemyPrefab;
    [SerializeField] private GoombaPool enemyPool;
    [SerializeField] private bool usePooling;
    [SerializeField] private bool spawnOnInput = true;
    [SerializeField] private float spawnInterval = 0.1f;
    [SerializeField] private int spawnAmount = 10;
    [SerializeField] private float enemyLifetime = 3f;

    private float spawnTimer;
    private bool pooledRun;

    void Start()
    {
        pooledRun = usePooling;

        if (pooledRun)
        {
            enemyPool.CreatePool();
        }
    }

    void Update()
    {
        if (spawnOnInput && !Input.GetKey(KeyCode.P))
        {
            return;
        }

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            Spawn();
            spawnTimer = spawnInterval;
        }
    }

    void Spawn()
    {
        Profiler.BeginSample("GoombaSpawner.Spawn");

        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 position = transform.position + Vector3.right * i * 1.25f;

            if (pooledRun)
            {
                enemyPool.GetEnemy(position, enemyLifetime);
            }
            else
            {
                Goomba enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
                enemy.SetLifetime(enemyLifetime);
            }
        }

        Profiler.EndSample();
    }
}