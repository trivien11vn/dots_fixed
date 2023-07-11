
using Unity.Collections;
using Unity.Entities;

public partial struct HealthSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.Temp);

        foreach (var (health, entity) in SystemAPI.Query<RefRO<Health>>().WithEntityAccess())
        {
            if (health.ValueRO.health <= 0)
            {
                ecb.AddComponent(entity, new Destroy { });
            }
        }

        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }

}