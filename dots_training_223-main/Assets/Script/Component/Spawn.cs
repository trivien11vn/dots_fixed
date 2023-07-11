using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct Spawn : IComponentData
{
    public Entity enemy;
    public float3 spawn_position;
    public bool canSpawn;
    public float nextSpawnTime;
    public float spawnRate;

}
