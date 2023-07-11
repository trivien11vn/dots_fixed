using CortexDeveloper.ECSMessages.Components;
using Unity.Entities;

/*IMessageComponent: Đây là một giao diện (interface) trong CortexDeveloper.ECSMessages.Components cho biết rằng
 cấu trúc này là một thành phần tin nhắn được sử dụng trong các tin nhắn liên quan đến hệ thống thực thể.*/
public struct StartCommand : IComponentData, IMessageComponent
{
    /*
    StartCommand command = new StartCommand();
    bool isStartValue = command.IsStart; (getter)
    command.IsStart = true; (setter)
    */
    public bool IsStart { get; set; }
}