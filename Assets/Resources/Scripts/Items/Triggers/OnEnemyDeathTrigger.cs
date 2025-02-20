
using System;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "OnEnemyDeathTrigger", menuName = "Items/Triggers/OnEnemyDeathTrigger")]
public class OnEnemyDeathTrigger : AEnemyTargetingTrigger
{
    protected override void AddEffectToEvent(UnityAction<AActor> effect, EnemyAI enemyScript)
    {
        enemyScript.Events.OnDeath.AddListener(() => effect(enemyScript));
    }
}

