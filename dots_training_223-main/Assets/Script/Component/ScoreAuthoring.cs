
using Unity.Entities;
using UnityEngine;

public class ScoreAuthoring : MonoBehaviour
{
    public float score;
}

//* This is a way to add component to the entity.
public class ScoreBaker : Baker<ScoreAuthoring>
{
    public override void Bake(ScoreAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new Score
        {
            score = authoring.score
        });
    }
}