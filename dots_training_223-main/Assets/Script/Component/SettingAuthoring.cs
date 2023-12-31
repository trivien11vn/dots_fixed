using Unity.Entities;
using UnityEngine;

public class SettingAuthoring : MonoBehaviour
{
    public float level;
}

public class SettingBaker : Baker<SettingAuthoring>
{
    public override void Bake(SettingAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new Setting
        {
            global_level = authoring.level,
        });
    }
}