using Unity.Entities;
using Unity.Rendering;
using UnityEngine;

public class SpawnAuthoring : MonoBehaviour
{
    public GameObject _entityPrefab;
    public Vector3 _position;
    public bool _canSpawn;
    public float _nextSpawnTime;
    public float _spawnRate;

}

public class SpawnBaker : Baker<SpawnAuthoring>
{
    public override void Bake(SpawnAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new Spawn
        {
            spawn_position = authoring._position,
            canSpawn = authoring._canSpawn,
            enemy = GetEntity(authoring._entityPrefab),
            nextSpawnTime = authoring._nextSpawnTime,
            spawnRate = authoring._spawnRate
        });


    }
}
