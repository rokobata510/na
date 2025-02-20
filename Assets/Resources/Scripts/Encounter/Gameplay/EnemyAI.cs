using System;
using System.Collections;
using UnityEngine;

public class EnemyAI : AActor
{
    [SerializeField] AEnemyMovementStrategy movementStrategy;
    public override AMovementStrategy MovementStrategy { get => movementStrategy; set => movementStrategy = (AEnemyMovementStrategy)value; }
    AEnemyMovementStrategy movementStrategyInstance;
    public override AMovementStrategy MovementStrategyInstance { get => movementStrategyInstance; set => movementStrategyInstance = (AEnemyMovementStrategy)value; }
    float TimeToMoveToNextStepInSeconds;
    float TimeSinceLastTargetingCallInSeconds = float.MaxValue;
    [SerializeField] EnemyEvents events = new();
    public override AttackableEvents Events
    {
        get
        {
            return events;
        }
        set
        {
            events = (EnemyEvents)value;
        }
    }
    protected bool GettingKnockedBack = false;
    public int spawnCost;
    public int spawnWeight;

    public override void Awake()
    {
        base.Awake();
        events.OnIdle.AddListener(() => animator.SetBool("isWalking", false));
        events.OnDamaged.AddListener((GameObject attackergameObject, IDealsDamage attackerProps) => StartCoroutine(GetKnockedBack(attackergameObject, attackerProps)));
        events.OnWeaponChange.Invoke();
        MovementStrategyInstance = movementStrategy.Clone();
        attackStrategyInstance = attackStrategy.Clone();
    }

    private IEnumerator GetKnockedBack(GameObject attackergameObject, IDealsDamage attackerProps)
    {
        GettingKnockedBack = true;
        NormalizedVector3 knockbackDirection = transform.position - attackergameObject.transform.position;
        rigidBody.AddForce(attackerProps.Damage * knockbackDirection, ForceMode2D.Impulse);
        while (rigidBody.velocity.magnitude > 0.5f)
        {
            yield return null;
        }
        GettingKnockedBack = false;

    }

    public void FixedUpdate()
    {
        FlipSpriteIfNeeded();
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
            AttackLogic();
        }
        if (health <= 0)
        {
            Die();
        }
        bool ExpectedToBeOnTarget() => TimeSinceLastTargetingCallInSeconds >= TimeToMoveToNextStepInSeconds;


    }

    protected override void GetNextStepTarget()
    {
        //TODO: oldd meg, hogy ne legyen choppy a movement (béna megoldás az az, hogy elfelezed a kövi lépésig az időt)
        TimeSinceLastTargetingCallInSeconds = 0;
        UnnormalizedVector3 positionRoundedToHalves = new(Mathf.Round(transform.position.x * 2f) / 2f, Mathf.Round(transform.position.y * 2f) / 2f);
        target = positionRoundedToHalves + MovementStrategyInstance.GetNextStep(gameObject);
        if (target == new UnnormalizedVector3(0f, 0f))
        {
            TimeToMoveToNextStepInSeconds = 1;
        }
        else
        {
            TimeToMoveToNextStepInSeconds = (Vector3.Distance(transform.position, target) / MovementSpeed);
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