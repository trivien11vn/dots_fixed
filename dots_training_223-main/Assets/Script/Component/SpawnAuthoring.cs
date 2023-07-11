using Unity.Entities;
using Unity.Rendering;
using UnityEngine;

public class SpawnAuthoring : MonoBehaviour
{
    public GameObject _enemy;
    public Vector3 _position;
    public bool _canSpawn;
}

public class SpawnBaker : Baker<SpawnAuthoring>
{
    public override void Bake(SpawnAuthoring authoring)
    {
        // authoring._spawnPosition.y = Random.Range(-2f, 5);

        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new Spawn
        {
            position = authoring._position,
            canSpawn = authoring._canSpawn,
            enemy = GetEntity(authoring._enemy)
        });


    }
}
