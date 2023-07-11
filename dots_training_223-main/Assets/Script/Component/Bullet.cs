using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public struct Bullet : IComponentData
{
    public float value_speed;
    public float value_damage;
}
[BurstCompile]
public partial struct MoveBullet : IJobEntity
{
    public float deltaTime;
    /*Trong ngôn ngữ lập trình C#, từ khóa "ref" được sử dụng để truyền 
    một tham chiếu (reference) của một đối tượng vào một phương thức.*/
    public void Execute(ref LocalTransform transform, Bullet bullet)
    {
        transform.Position.y += bullet.value_speed * deltaTime;
        // Rotate y
        transform.Rotation = Quaternion.Euler(0, 90 * deltaTime, 0);

    }
}
