using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Rendering;

public struct Material_Mes : IComponentData
{
    public BatchMaterialID materialID;
    public BatchMeshID meshID;
}
