using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;
using System;
using Unity.Collections;
using Unity.Rendering;





public partial struct SpawnSystem : ISystem
{
    EntityQuery componentQuery;
    EntityQuery query;
    float CurrentLevel;

    [BurstCompile]
    public partial struct SpawnJob : IJobEntity
    {
        // variable
        public EntityCommandBuffer.ParallelWriter ecb;
        public float numberOfEnemyInScene;
        public float level;

        public void Execute(ref LocalTransform tf, ref Spawn spawner)
        {
            // action
            //spawner.canSpawn kiểm tra có thể tạo enemy mới hay không
            if (spawner.canSpawn && (numberOfEnemyInScene == 0))
            {
                //ProcessSpawn được gọi để tạo ra các enemy mới
                ProcessSpawn(ref ecb, spawner);
            }
        }


        private void ProcessSpawn(ref EntityCommandBuffer.ParallelWriter ecb, Spawn spawner)
        {
            Entity enemyPrefab = spawner.enemyEntity;
            switch (level)
            {
                case 0:                 
                    float sideA = 6f;
                    float sideB = 8f;
                    float sideC = 10f;
                    int numPoint = 30;
                    Vector2 pointA = Vector2.zero;
                    Vector2 pointB = new Vector2(sideA, 0);
                    float cosC = (sideA * sideA + sideB * sideB - sideC * sideC) / (2 * sideA * sideB);
                    float sinC = Mathf.Sqrt(1 - cosC * cosC);

                    Vector2 pointC = new Vector2(sideB * cosC, sideB * sinC);
                    // Đặt z=0 cho các điểm
                    Vector3 point3D_A = new Vector3(pointA.x, pointA.y, 0);
                    ecb.SetComponent(0,enemyPrefab, LocalTransform.FromPosition(new float3{x = point3D_A.x,y = point3D_A.y,z = point3D_A.z}).WithScale(0.5f));
                    ecb.Instantiate(0,enemyPrefab);
                    Vector3 point3D_B = new Vector3(pointB.x, pointB.y, 0);
                    ecb.SetComponent(1,enemyPrefab, LocalTransform.FromPosition(new float3{x = point3D_B.x,y = point3D_B.y,z = point3D_B.z}).WithScale(0.5f));
                    ecb.Instantiate(1,enemyPrefab);
                    Vector3 point3D_C = new Vector3(pointC.x, pointC.y, 0);
                    ecb.SetComponent(2,enemyPrefab, LocalTransform.FromPosition(new float3{x = point3D_C.x,y = point3D_C.y,z = point3D_C.z}).WithScale(0.5f));
                    ecb.Instantiate(2,enemyPrefab);
                    int rest_numPoint = numPoint - 3;
                    int j1 = 2;
                    for (int i = 1; i <= rest_numPoint; i++)
                    {
                        float t = i / (float)(rest_numPoint + 1);
                        Vector3 pointOnAB = Vector3.Lerp(point3D_A, point3D_B, t);
                        j1++;
                        ecb.SetComponent(j1,enemyPrefab, LocalTransform.FromPosition(new float3{x = pointOnAB.x,y = pointOnAB.y,z = pointOnAB.z}).WithScale(0.5f));
                        ecb.Instantiate(j1,enemyPrefab);
                        Vector3 pointOnBC = Vector3.Lerp(point3D_B, point3D_C, t);
                        j1++;
                        ecb.SetComponent(j1,enemyPrefab, LocalTransform.FromPosition(new float3{x = pointOnBC.x,y = pointOnBC.y,z = pointOnBC.z}).WithScale(0.5f));
                        ecb.Instantiate(j1,enemyPrefab);
                        Vector3 pointOnCA = Vector3.Lerp(point3D_C, point3D_A, t);
                        j1++;
                        ecb.SetComponent(j1,enemyPrefab, LocalTransform.FromPosition(new float3{x = pointOnCA.x,y = pointOnCA.y,z = pointOnCA.z}).WithScale(0.5f));
                        ecb.Instantiate(j1,enemyPrefab);
                    }
                    break;
                case 1:
                    float size = 5f; // kich thuoc hinh chu X
                    ecb.SetComponent(0,enemyPrefab, LocalTransform.FromPosition(new float3{x = 0f,y = 0f,z = 0f}).WithScale(0.5f));
                    ecb.Instantiate(0,enemyPrefab);

                    float offset = size/2f;
                    ecb.SetComponent(1,enemyPrefab, LocalTransform.FromPosition(new float3{x=-offset,y=offset,z=0}).WithScale(0.5f));
                    ecb.Instantiate(1,enemyPrefab);
                    ecb.SetComponent(2,enemyPrefab, LocalTransform.FromPosition(new float3{x=offset,y=offset,z=0}).WithScale(0.5f));
                    ecb.Instantiate(2,enemyPrefab);
                    ecb.SetComponent(3,enemyPrefab, LocalTransform.FromPosition(new float3{x=-offset,y=-offset,z=0}).WithScale(0.5f));
                    ecb.Instantiate(3,enemyPrefab);
                    ecb.SetComponent(4,enemyPrefab, LocalTransform.FromPosition(new float3{x=offset,y=-offset,z=0}).WithScale(0.5f));
                    ecb.Instantiate(4,enemyPrefab);
                    ecb.SetComponent(5,enemyPrefab, LocalTransform.FromPosition(new float3{x=-offset,y=0,z=0}).WithScale(0.5f));
                    ecb.Instantiate(5,enemyPrefab);
                    ecb.SetComponent(6,enemyPrefab, LocalTransform.FromPosition(new float3{x=offset,y=0,z=0}).WithScale(0.5f));
                    ecb.Instantiate(6,enemyPrefab);
                    ecb.SetComponent(7,enemyPrefab, LocalTransform.FromPosition(new float3{x=0,y=offset,z=0}).WithScale(0.5f));
                    ecb.Instantiate(7,enemyPrefab);
                    ecb.SetComponent(8,enemyPrefab, LocalTransform.FromPosition(new float3{x=0,y=-offset,z=0}).WithScale(0.5f));
                    ecb.Instantiate(8,enemyPrefab);
                    float diagonalOffset = size / (2f * Mathf.Sqrt(2));
                    ecb.SetComponent(9,enemyPrefab, LocalTransform.FromPosition(new float3{x=-diagonalOffset,y=diagonalOffset,z=0}).WithScale(0.5f));
                    ecb.Instantiate(9,enemyPrefab);
                    ecb.SetComponent(10,enemyPrefab, LocalTransform.FromPosition(new float3{x=diagonalOffset,y=diagonalOffset,z=0}).WithScale(0.5f));
                    ecb.Instantiate(10,enemyPrefab);
                    ecb.SetComponent(11,enemyPrefab, LocalTransform.FromPosition(new float3{x=-diagonalOffset,y=-diagonalOffset,z=0}).WithScale(0.5f));
                    ecb.Instantiate(11,enemyPrefab);
                    ecb.SetComponent(12,enemyPrefab, LocalTransform.FromPosition(new float3{x=diagonalOffset,y=-diagonalOffset,z=0}).WithScale(0.5f));
                    ecb.Instantiate(12,enemyPrefab);
                    break;
                case 2:
                    float width = 20f; // Chiều dài của hình chữ nhật
                    float height = 10f; // Chiều rộng của hình chữ nhật
                    int numPoints = 20; // Số lượng điểm trên hình chữ nhật

                    float minX = -width / 2f; // Tọa độ X của góc dưới bên trái của hình chữ nhật
                    float minY = -height / 2f; // Tọa độ Y của góc dưới bên trái của hình chữ nhật

                    int numColumns = Mathf.CeilToInt(Mathf.Sqrt(numPoints)); // Số lượng cột trên lưới
                    int numRows = Mathf.CeilToInt((float)numPoints / numColumns); // Số lượng hàng trên lưới

                    float cellWidth = width / numColumns; // Chiều rộng của mỗi ô trên lưới
                    float cellHeight = height / numRows; // Chiều cao của mỗi ô trên lưới

                    int pointIndex = 0; // Chỉ số điểm

                    for (int i = 0; i < numRows; i++)
                    {
                        for (int j = 0; j < numColumns; j++)
                        {
                            if (pointIndex >= numPoints)
                                break;

                            var pX = minX + (j * cellWidth); // Tọa độ X của điểm
                            var pY = minY + (i * cellHeight); // Tọa độ Y của điểm
                            float3 position = new float3
                            {
                                x = pX / 5,
                                y = pY / 5 + 0.5f,
                                z = 0
                            };

                            ecb.SetComponent(i, enemyPrefab, LocalTransform.FromPosition(position).WithScale(1f));
                            ecb.Instantiate(i, enemyPrefab);
                            pointIndex++;
                        }
                    }
                    break;
                case 3:
                    var radiuss = 4f; // Bán kính của hình tròn
                    var numberOfEnemies = 10; // Số lượng enemy trong hình tròn
                    for (int i = 0; i < numberOfEnemies; i++)
                    {
                        var angle = (2 * Mathf.PI * i) / numberOfEnemies; // Góc của từng enemy trên hình tròn
                        var posX = radiuss * Mathf.Cos(angle); // Tọa độ X của enemy
                        var posY = radiuss * Mathf.Sin(angle); // Tọa độ Y của enemy

                        float3 position = new float3
                        {
                            x = posX,
                            y = posY,
                            z = 0
                        };
                        ecb.SetComponent(i, enemyPrefab, LocalTransform.FromPosition(position).WithScale(0.5f));
                        ecb.Instantiate(i, enemyPrefab);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void OnCreate(ref SystemState state)
    {
        query = new EntityQueryBuilder(Allocator.Temp)
                    .WithAll<Enemy>()
                    .WithAll<LocalTransform>()
                    .Build(ref state);
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.TempJob);
        var numberOfEnemyInScene = query.CalculateEntityCount();

        if (numberOfEnemyInScene == 0)
        {
            CurrentLevel = LevelUp(ref state);
        }
        state.Dependency = new SpawnJob
        {
            ecb = ecb.AsParallelWriter(),
            numberOfEnemyInScene = numberOfEnemyInScene,
            level = CurrentLevel
        }.ScheduleParallel(state.Dependency);

        state.Dependency.Complete();
        ecb.Playback(state.EntityManager);
        ecb.Dispose();

    }

    private float LevelUp(ref SystemState state)
    {
        var gameConfig = SystemAPI.GetSingleton<GameConfig>();
        gameConfig._level += 1;
        SystemAPI.SetSingleton(gameConfig);
        return gameConfig._level;
    }


}
