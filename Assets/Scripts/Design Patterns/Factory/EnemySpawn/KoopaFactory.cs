using UnityEngine;
using UnityEngine.Profiling;

public class KoopaFactory : EnemyFactory
{
    [SerializeField] private Koopa enemyPrefab;
    [SerializeField] private EnemyPool enemyPool;

    private bool pooledRun;

    public override void Prepare(bool usePooling)
    {
        pooledRun = usePooling;

        if (pooledRun)
        {
            enemyPool.CreatePool(enemyPrefab);
        }
    }

    public override Enemy SpawnEnemy(Vector3 position, float lifetime)
    {
        Profiler.BeginSample("KoopaFactory.Spawn");

        Enemy enemy;

        if (pooledRun)
        {
            enemy = enemyPool.GetEnemy(position, lifetime);
        }
        else
        {
            enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            enemy.SetLifetime(lifetime);
        }

        Profiler.EndSample();
        return enemy;
    }
}
