
using Unity.Collections;
using Unity.Entities;
using Unity.Rendering;

public partial struct MatAndMesSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        // Assign material

    }

    public void OnUpdate(ref SystemState state)
    {
        /*Hàm "OnUpdate" của hệ thống "MatAndMesSystem" có một tham số truyền vào là "ref SystemState state". Điều này 
        cho phép hệ thống truy cập và thay đổi trạng thái (state) của chính nó trong quá trình cập nhật.*/
        foreach (var (matAndMes, matmeshinfo, entity) in SystemAPI.Query<RefRO<Material_Mes>, RefRW<MaterialMeshInfo>>().WithEntityAccess())
        {
            matmeshinfo.ValueRW.MaterialID = matAndMes.ValueRO.materialID;
            matmeshinfo.ValueRW.MeshID = matAndMes.ValueRO.meshID;
        }
    }
}