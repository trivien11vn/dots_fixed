using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct Health : IComponentData
{
    public float health;
    public float protect_hp;
}
