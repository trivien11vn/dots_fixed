using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct Spawn : IComponentData
{
    public Entity enemy;
    public float3 position;
    public bool canSpawn;
}
