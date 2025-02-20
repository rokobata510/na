
//using System.Collections;
//using UnityEngine;
//[CreateAssetMenu(fileName = "StackingDamageOverTimeEffect", menuName = "Items/Effects/ActorTargeting/StackingDamageOverTimeEffect")]
//public class DamageOverTimeEffect : AActorTargetingEffect, IDealsDamage
//{

//    public int damage;
//    public int Damage { get => damage; set => damage = value; }
//    public float knockback;
//    public float Knockback { get => knockback; set => knockback = value; }
//    public bool givesInvincibilityFrames;
//    public bool GivesInvincibilityFrames { get => givesInvincibilityFrames; set => givesInvincibilityFrames = value; }
//    public int ticks;
//    public float secondsBetweenDamageTicks;
//    public bool stacks;

//    public override void TriggerAActorTargetingEffect(AActor target)
//    {
//        GameObject.Find("ItemEffectDirector").GetComponent<ItemEffectDirector>().TriggerCoroutine(DamageOverTimeCoroutine(target));
//    }
//    protected virtual IEnumerator DamageOverTimeCoroutine(AActor target)
//    {
//        if (GameObject.Find("ItemEffectDirector").GetComponent<ItemEffectDirector>().currentlyRunningCoroutines.Contains((IEnumerator)this)&& !stacks)
//        {
//            yield break;
//        }
//        for (int i = 0; i < ticks; i++)
//        {
//            //target.GetAttacked(this);
//            yield return new WaitForSeconds(secondsBetweenDamageTicks);
//        }
//    }

//    public void TryToDealDamage(GameObject DealDamageToThisTarget)
//    {
//        if (DealDamageToThisTarget.TryGetComponent(out AAttackable attackable))
//        {
//            attackable.GetAttacked(DealDamageToThisTarget, this);
//        }
//    }
//}

