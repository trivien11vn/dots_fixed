using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Jobs;
using System;
using UnityEngine;
using Unity.Mathematics;


[RequireMatchingQueriesForUpdate]
public partial struct EnemySystem : ISystem
{
    float deltaTime;
    EntityQuery enemyQuery;

    public void OnCreate(ref SystemState state)
    {
        deltaTime = SystemAPI.Time.DeltaTime;


        enemyQuery = new EntityQueryBuilder(Allocator.Temp)
            .WithAllRW<Enemy>()
            .WithAll<LocalTransform>()
            .WithOptions(EntityQueryOptions.FilterWriteGroup)
            .Build(ref state);
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        JobSystemToMoveEnemy(ref state);
    }

    private void JobSystemToMoveEnemy(ref SystemState state)
    {
        var _job = new EnemyMovingJob
        {
            deltaTime = SystemAPI.Time.DeltaTime
        };
        _job.ScheduleParallel(enemyQuery);

    }
}
