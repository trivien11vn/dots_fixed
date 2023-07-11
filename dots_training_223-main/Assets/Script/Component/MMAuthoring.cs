using Unity.Entities;
using Unity.Rendering;
using UnityEngine;


public class MMAuthoring : MonoBehaviour
{
    public Mesh mesh;
    public Material material;
}

public class Material_MesBaker : Baker<MMAuthoring>
{
    public override void Bake(MMAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);

        /*Biến "hybirdRender" được khởi tạo để lấy tham chiếu tới hệ thống "Entities​Graphics​System" (hay còn gọi là Hybrid
        Renderer) từ thế giới (World) mặc định của GameObject hiện tại. 
        Hệ thống "Entities​Graphics​System" được sử dụng để quản lý việc hiển thị đối tượng được tạo ra từ Unity Entities 
        framework.*/
        var hybirdRender = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<Entities​Graphics​System>();

        /*Sau đó, thông tin về Mesh và Material được đăng ký thông qua hệ thống "Entities​Graphics​System". Điều này được 
        thực hiện bằng cách sử dụng phương thức "RegisterMesh" và "RegisterMaterial" của biến "hybirdRender" để đăng ký 
        Mesh và Material từ đối tượng "MatAndMesAuthoring"*/
        AddComponent(entity, new Material_Mes
        {
            meshID = hybirdRender.RegisterMesh(authoring.mesh),
            materialID = hybirdRender.RegisterMaterial(authoring.material)
        });
    }
}