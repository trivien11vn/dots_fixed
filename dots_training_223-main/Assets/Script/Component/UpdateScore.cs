using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Entities;
using UnityEngine;

public class UpdateScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {

    }
    void Update()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        
        var entity_score = entityManager.CreateEntityQuery(typeof(Score)).GetSingleton<Score>();
        scoreText.text = entity_score.score.ToString();
    }
}
