using System;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Experimental.GraphView;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyAI : AActor
{
    [SerializeField] AEnemyMovementStrategy movementStrategy;
    public override AMovementStrategy MovementStrategy { get => movementStrategy; set => movementStrategy = (AEnemyMovementStrategy)value; }
    AEnemyMovementStrategy movementStrategyInstance;
    protected override AMovementStrategy MovementStrategyInstance { get => movementStrategyInstance; set => movementStrategyInstance = (AEnemyMovementStrategy)value; }
    float TimeToMoveToNextStepInSeconds;
    float TimeSinceLastTargetingCallInSeconds = float.MaxValue;
    [SerializeField] EnemyEvents events = new();
    public override AttackableEvents Events { get => events; set => events = (EnemyEvents)value; }
    protected bool GettingKnockedBack = false;

    public new void Start()
    {
        base.Start();
        events.OnIdle.AddListener(() => animator.SetBool("isWalking", false));
        events.OnDamaged.AddListener((ADealsDamage attacker) => StartCoroutine(GetKnockedBack(attacker)));
        MovementStrategyInstance = movementStrategy.Clone();
        attackStrategyInstance = attackStrategy.Clone();
    }

    private IEnumerator GetKnockedBack(ADealsDamage attacker)
    {
        GettingKnockedBack = true;
        NormalizedVector3 knockbackDirection = transform.position - attacker.transform.position;
        rigidBody.AddForce(attacker.Damage* knockbackDirection, ForceMode2D.Impulse);
        while (rigidBody.velocity.magnitude > 0.5f)
        {
            yield return null;
        }
        GettingKnockedBack = false;

    }

    public void FixedUpdate()
    {
        if (!GettingKnockedBack)
        {
            if (ExpectedToBeOnTarget())
            {
                GetNextStepTarget();
            }
            else
            {
                MoveToTarget();
                TimeSinceLastTargetingCallInSeconds += Time.deltaTime;
            }
        }
        FlipSpriteIfNeeded();
        if (health <= 0)
        {
            Die();
        }
        AttackLogic();
        bool ExpectedToBeOnTarget() => TimeSinceLastTargetingCallInSeconds>=TimeToMoveToNextStepInSeconds;
        

    }

    protected override void GetNextStepTarget()
    {
        //TODO: oldd meg, hogy ne legyen choppy a movement (béna megoldás az az, hogy elfelezed a kövi lépésig az időt)
        TimeSinceLastTargetingCallInSeconds = 0;
        UnnormalizedVector3 positionRoundedToHalves = new(Mathf.Round(transform.position.x * 2f) / 2f, Mathf.Round(transform.position.y * 2f) / 2f);
        target = positionRoundedToHalves + MovementStrategyInstance.GetNextStep(positionRoundedToHalves, (UnnormalizedVector3)transform.lossyScale); 
        if (target == new UnnormalizedVector3(0f, 0f))
        {
            TimeToMoveToNextStepInSeconds = 1;
        }
        else
        {
            TimeToMoveToNextStepInSeconds = (Vector3.Distance(transform.position, target) / movementSpeed)/2;
        }
    
    }
    /*
protected override void AttackLogic()
{
   if (SightChecker.CanSeeTarget(transform.position, target, 20))
   {
       base.AttackLogic();
   }
}
*/


}