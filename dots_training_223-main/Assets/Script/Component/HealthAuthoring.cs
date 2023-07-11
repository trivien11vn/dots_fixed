using Unity.Entities;
using UnityEngine;


public class HealthAuthoring : MonoBehaviour
{
    public float health;

    public float protect;
}

public class HealthBaker : Baker<HealthAuthoring>
{
    public override void Bake(HealthAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new Health
        {
            health = authoring.health,
            protect_hp = authoring.protect
        });
    }
}