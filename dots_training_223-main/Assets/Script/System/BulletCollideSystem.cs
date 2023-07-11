using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using UnityEngine;


namespace Systems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(SimulationSystemGroup))]
    public partial struct BulletCollideSystem : ISystem
    {
        //OnCreate là phương thức được gọi khi hệ thống được tạo
        public void OnCreate(ref SystemState state)
        {
            //hệ thống yêu cầu các thành phần Enemy, Bullet và SimulationSingleton cho việc cập nhật.

            /*Thành phần SimulationSingleton được yêu cầu để chỉ định rằng hệ thống cần truy cập vào 
            thế giới vật lý (physics world) trong quá trình xử lý va chạm.*/
            state.RequireForUpdate<Enemy>();
            state.RequireForUpdate<Bullet>();
            state.RequireForUpdate<SimulationSingleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            /*EntityCommandBuffer là một cơ chế trong Unity.Entities cho phép bạn ghi lại các thay đổi của thực thể 
            (entity) trong một hệ thống (system) mà không ảnh hưởng trực tiếp đến dữ liệu của các thực thể đó. Thay vì
            thực hiện thay đổi trực tiếp trên thực thể trong vòng lặp, bạn có thể ghi lại các thao tác thay đổi và 
            sau đó tái áp dụng chúng vào EntityManager sau khi hệ thống đã hoàn thành.*/

            /*Allocator.TempJob được sử dụng để chỉ định cách phân bổ bộ nhớ cho EntityCommandBuffer. Allocator.
            TempJob chỉ định rằng bộ nhớ sẽ được cấp phát tạm thời và sẽ được giải phóng sau khi EntityCommandBuffer 
            được sử dụng xong. */
            var ecb = new EntityCommandBuffer(Allocator.TempJob);

            

            var setting = SystemAPI.GetSingletonEntity<Setting>();

            //* Dependency is a way to make sure that the job is finished before the next job is started.
            state.Dependency = new JobCheckCollision
            {
                ecb = ecb,
                enemyLookup = state.GetComponentLookup<Enemy>(),
                bulletLookup = state.GetComponentLookup<Bullet>(),
            }.Schedule(
                //SystemAPI.GetSingleton<SimulationSingleton>() được truyền vào làm đối số để cung cấp thế giới vật lý cho job JobCheckCollision.
                SystemAPI.GetSingleton<SimulationSingleton>(),

                /*state.Dependency được truyền vào để chỉ định rằng job JobCheckCollision phải chờ đợi job trước đó 
                hoàn thành trước khi nó có thể được thực thi. Điều này đảm bảo rằng kết quả của các job trước đó được sử 
                dụng đúng và không có xung đột dữ liệu xảy ra.*/
                state.Dependency
                );

            //* Complete is a way to make sure that the job is finished before the next job is started.
            state.Dependency.Complete();

            //* Playback is a way to play the command buffer. 
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }

    /*ITriggerEventsJob được sử dụng để xác định rằng struct JobCheckCollision là một job trong hệ thống 
    xử lý va chạm và nó cần truy cập vào sự kiện trigger (trigger event).*/
    internal struct JobCheckCollision : ITriggerEventsJob
    {
        //* EntityCommandBuffer is a way to create, delete, and modify entities from a job.
        public EntityCommandBuffer ecb { get; set; }

        //* When you want to passing a array of component, you need to use ComponentLookup
        public ComponentLookup<Enemy> enemyLookup { get; set; }
        public ComponentLookup<Bullet> bulletLookup { get; set; }

        // public float _dame;

        public void Execute(TriggerEvent triggerEvent)
        {

            /*EntityA và EntityB là hai thực thể liên quan đến sự kiện va chạm (TriggerEvent). Khi có một sự kiện va 
            chạm xảy ra, thông qua TriggerEvent, ta có thể truy cập các thông tin về các thực thể tham gia vào va chạm.*/
            var isBulletHitEnemy = (bulletLookup.HasComponent(triggerEvent.EntityA) && enemyLookup.HasComponent(triggerEvent.EntityB)) || (bulletLookup.HasComponent(triggerEvent.EntityB) && enemyLookup.HasComponent(triggerEvent.EntityA));

            if (isBulletHitEnemy)
            {
                if (enemyLookup.HasComponent(triggerEvent.EntityA))
                {

                    ecb.AddComponent(triggerEvent.EntityB, new Destroy { });
                    ecb.AddComponent(triggerEvent.EntityA, new IncrementScore());
                    ecb.AddComponent(triggerEvent.EntityA, new Damage
                    {
                        Value = 5
                    });

                }
                if (enemyLookup.HasComponent(triggerEvent.EntityB))
                {
                    ecb.AddComponent(triggerEvent.EntityA, new Destroy { });
                    ecb.AddComponent(triggerEvent.EntityB, new IncrementScore());
                    ecb.AddComponent(triggerEvent.EntityB, new Damage
                    {
                        Value = 5
                    });

                }
            }
        }
    }
}

