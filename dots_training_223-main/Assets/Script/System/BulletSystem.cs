using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial struct BulletSystem : ISystem
{
    float deltaTime;
    public EntityQuery bulletQuery;
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        deltaTime = SystemAPI.Time.DeltaTime;
        // Allocator.Temp: chi dinh bo nho duoc cap phat tam thoi va se giai phong sau khi truy van hoan thanh
        bulletQuery = new EntityQueryBuilder(Allocator.Temp)
    .WithAllRW<Bullet>()
    .WithAll<LocalTransform>()
    .WithOptions(EntityQueryOptions.FilterWriteGroup)
    .Build(ref state);
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        //! Query
        // MoveBullet(ref state);

        //! Job System
        JobMoveBullet(ref state);
        // complete the job
        state.Dependency.Complete();

        //if bullet above 10, destroy it
        DestroyBullet(ref state);

    }

    private void DestroyBullet(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.TempJob);

        // WithEntityAccess() được sử dụng trong lệnh foreach để cung cấp truy cập đến các thành phần của entity trong quá trình lặp.
        // Lệnh foreach trong đoạn mã lấy ra các entity có cả hai thành phần LocalTransform và Bullet
        foreach (var (bullet, tf, entity) in SystemAPI.Query<RefRO<Bullet>, RefRO<LocalTransform>>().WithEntityAccess())
        {
            if (tf.ValueRO.Position.y > 10)
            {
                ecb.DestroyEntity(entity);
            }
        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }

    private void JobMoveBullet(ref SystemState state)
    {
        var bulletJob = new MoveBullet
        {
            deltaTime = SystemAPI.Time.DeltaTime
        };
        //* ScheduleParallel is a way to run the job in parallel.
        bulletJob.ScheduleParallel(bulletQuery);
    }

    // private void MoveBullet(ref SystemState state)
    // {
    //     //* Query is a way to get the component from the entity.
    //     foreach (var (transform, speed, entity) in SystemAPI.Query<
    //             RefRW<LocalTransform>,
    //             RefRW<Bullet>
    //             >().WithEntityAccess())
    //     {
    //         transform.ValueRW.Position.y += speed.ValueRO.speed * deltaTime;

    //         // Rotate y
    //         //* Quaternion Euler is degree in Unity.
    //         transform.ValueRW.Rotation = Quaternion.Euler(0, 90 * deltaTime, 0);
    //     }
    // }

}

