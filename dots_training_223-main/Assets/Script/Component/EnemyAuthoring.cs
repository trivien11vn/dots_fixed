using Unity.Entities;
using UnityEngine;

public class EnemyAuthoring : MonoBehaviour
{
    public float value_speed;
    public Material value_Red;
    public Material value_Green;
}

public class EnemyBaker : Baker<EnemyAuthoring>
{
    public override void Bake(EnemyAuthoring authoring)
    {

        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new Enemy { speed = authoring.value_speed });

    }
}
