using JetBrains.Annotations;
using UnityEngine;

public abstract class EnemyFactory : MonoBehaviour
{
    public abstract void Prepare(bool usePooling);

    public abstract Enemy SpawnEnemy(Vector3 position, float lifetime);

}
