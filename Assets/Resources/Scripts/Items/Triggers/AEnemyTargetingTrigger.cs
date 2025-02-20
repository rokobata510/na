
using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class AEnemyTargetingTrigger : AActorTargetingTrigger
{
    public sealed override void RegisterActorTargetingTrigger(UnityAction<AActor> effect, ItemEffectDirector director, AActor subject)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            EnemyAI enemyScript = enemy.GetComponent<EnemyAI>();
            AddEffectToEvent(effect, enemyScript);
        }
    }

    protected abstract void AddEffectToEvent(UnityAction<AActor> effect, EnemyAI enemyScript);
}
