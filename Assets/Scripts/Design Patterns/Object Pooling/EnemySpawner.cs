using UnityEngine;
using UnityEngine.Profiling;

public class EnemySpawner : MonoBehaviour
{
    public enum SpawnMode
    {
        PoolingOnly, 
        FactoryOnly,
        FactoryWithPooling

    }

    [SerializeField] private SpawnMode spawnMode = SpawnMode.FactoryWithPooling;
    [SerializeField] private EnemyFactory goombaFactory;
    [SerializeField] private EnemyFactory koopaFactory;
    [SerializeField] private EnemyPool goombaPool;
    [SerializeField] private EnemyPool koopaPool;
    [SerializeField] private Goomba goombaPrefab; 
    [SerializeField] private Koopa koopaPrefab;
    [SerializeField] private bool spawnOnInput;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private int spawnAmount = 1;
    [SerializeField] private float enemyLifetime = 30f;
    [SerializeField] private bool repeatableTest;
    [SerializeField] private int randomSeed = 1234;
    [SerializeField] private int successfulSpawns;
    [SerializeField] private int skippedSpawns;

    private float spawnTimer;
    private SpawnMode currentMode;

    void Start()
    {
        currentMode = spawnMode;
        successfulSpawns = 0;
        skippedSpawns = 0;

        if (repeatableTest)
        {
            UnityEngine.Random.InitState(randomSeed);
        }

        if (currentMode == SpawnMode.PoolingOnly)
        {
            goombaPool.CreatePool(goombaPrefab);
            koopaPool.CreatePool(koopaPrefab);
        }
        else
        {
            bool usePooling = currentMode == SpawnMode.FactoryWithPooling;

            goombaFactory.Prepare(usePooling);
            koopaFactory.Prepare(usePooling);
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
        Profiler.BeginSample("EnemySpawner.Spawn");

        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 position = transform.position + Vector3.right * i * 1.25f;

            EnemyFactory factory;
            EnemyPool pool;

            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                factory = goombaFactory;
                pool = goombaPool;
            }
            else
            {
                factory = koopaFactory;
                pool = koopaPool;
            }

            Enemy enemy;

            if (currentMode == SpawnMode.PoolingOnly)
            {
                enemy = pool.GetEnemy(position, enemyLifetime);
            }
            else
            {
                enemy = factory.SpawnEnemy(position, enemyLifetime);
            }
        }

        Profiler.EndSample();
    }
}