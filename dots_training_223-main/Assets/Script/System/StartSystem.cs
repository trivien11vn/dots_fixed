using Unity.Entities;
using UnityEngine;

public partial struct StartSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<StartCommand>();
    }

    public void OnUpdate(ref SystemState state)
    {
        new StartGameCommandListenerJob().Schedule();
        /*sau khi hệ thống được cập nhật lần đầu tiên, nó sẽ bị vô hiệu hóa (disabled). Việc này ngăn hệ thống được cập nhật trong các vòng lặp tiếp theo. */
        state.Enabled = false;
    }
}

public partial struct StartGameCommandListenerJob : IJobEntity
{
    /*in StartCommand command: Đối số đầu vào cho phương thức, là một tham chiếu chỉ đọc (readonly) đến thành phần StartCommand.*/
    public void Execute(in StartCommand command)
    {
        Debug.Log($"Game started!");
    }
}