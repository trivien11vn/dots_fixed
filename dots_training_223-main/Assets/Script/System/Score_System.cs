
using CortexDeveloper.ECSMessages.Service;
using Unity.Collections;
using Unity.Entities;

public partial struct Score_System : ISystem
{
    public void OnCreate(ref SystemState state)
    {
    }

    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.TempJob);

        foreach (var (scoring, enemy, entity) in SystemAPI.Query<RefRW<IncrementScore>, RefRO<Enemy>>().WithEntityAccess())
        {
            var incre_score = SystemAPI.GetSingleton<Score>();
            incre_score.score = int.Parse(incre_score.score.ToString()) + 1f;
            SystemAPI.SetSingleton<Score>(incre_score);
            ecb.RemoveComponent<IncrementScore>(entity);
        }
        ecb.Playback(state.EntityManager);
    }

    public void OnDestroy(ref SystemState state)
    {
    }


}