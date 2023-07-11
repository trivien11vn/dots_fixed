
using Unity.Entities;
using UnityEngine;

public class ScoreAuthoring : MonoBehaviour
{
    public float _score;
}

//* This is a way to add component to the entity.
public class ScoringBaker : Baker<ScoreAuthoring>
{
    public override void Bake(ScoreAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new Scoring
        {
            score = authoring._score
        });
    }
}