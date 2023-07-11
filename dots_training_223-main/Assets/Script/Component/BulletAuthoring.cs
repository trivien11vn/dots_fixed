using Unity.Entities;
using UnityEngine;

public class BulletAuthoring : MonoBehaviour
{
    public float value_speed;
    public float value_damage;
}

public class BulletBaker : Baker<BulletAuthoring>
{
    public override void Bake(BulletAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new Bullet
        {
            value_speed = authoring.value_speed,
            value_damage = authoring.value_damage
        });
    }
}

