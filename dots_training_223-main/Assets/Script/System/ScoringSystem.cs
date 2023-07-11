
using CortexDeveloper.ECSMessages.Service;
using Unity.Collections;
using Unity.Entities;

public partial struct ScoringSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
    }

    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.TempJob);

        foreach (var (scoring, enemy, entity) in SystemAPI.Query<RefRW<IncrementScore>, RefRO<Enemy>>().WithEntityAccess())
        {
            var _scoreConfig = SystemAPI.GetSingleton<Scoring>();
            var a = int.Parse(_scoreConfig.score.ToString()) + 1f;
            // convert a to string
            _scoreConfig.score = a;
            SystemAPI.SetSingleton<Scoring>(_scoreConfig);
            ecb.RemoveComponent<IncrementScore>(entity);
        }
        ecb.Playback(state.EntityManager);
    }

    public void OnDestroy(ref SystemState state)
    {
        // throw new System.NotImplementedException();

    }


}