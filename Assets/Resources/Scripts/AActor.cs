using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public abstract class AActor : ACanBeAttacked
{

    public GameObject weapon;
    protected AWeapon weaponScript;
    public AAttackStrategy attackStrategy;
    protected AAttackStrategy attackStrategyInstance;
    public float movementSpeed;
    public abstract AMovementStrategy MovementStrategy { get; set; }
    protected abstract AMovementStrategy MovementStrategyInstance { get; set; }
    protected bool IsWalking => target != (UnnormalizedVector3)transform.position;

    protected Animator animator;
    public UnnormalizedVector3 target = new();


    public override void Start()
    {
        base.Start();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameObject weaponGameObject = InstantiateWeapon(weapon);
        weaponScript = weaponGameObject.GetComponent<AWeapon>();
        attackStrategyInstance = attackStrategy.Clone();
        MovementStrategyInstance = MovementStrategy.Clone();
        ((ActorEvents)Events).OnIdle.AddListener(() => animator.SetBool("isWalking", false));
        ((ActorEvents)Events).OnWalking.AddListener(() => animator.SetBool("isWalking", true));
    }
    protected virtual void AttackLogic() 
    {
        attackStrategyInstance.SetTarget(origin: (UnnormalizedVector3)transform.position);
        weaponScript.Point((UnnormalizedVector3)transform.position, attackStrategyInstance.targetDirection);
        if (attackStrategyInstance.WantsToAttack((UnnormalizedVector3)transform.position))
        {
            weaponScript.Attack(attackStrategyInstance.targetDirection);
        }
        else if(weaponScript is AProjectileWeapon typeCastWeaponScript)
        {
            typeCastWeaponScript.DontAttack();
        }
    }

    public GameObject InstantiateWeapon(GameObject weapon)
    {
        GameObject newWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        newWeapon.transform.lossyScale.Set(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);
        newWeapon.layer = gameObject.layer;
        newWeapon.tag = gameObject.tag;
        return newWeapon;
    }

    public override void Die()
    {
        Destroy(gameObject);
        weaponScript.Die();
    }
     protected void FlipSpriteIfNeeded()
    {
        if (target.X < 0)
            spriteRenderer.flipX = true;
        else if (target.X > 0)
            spriteRenderer.flipX = false;
    }
    protected virtual void GetNextStepTarget() => target = transform.position + MovementStrategyInstance.GetNextStep(transform.position, transform.lossyScale);
    protected virtual void MoveToTarget()
    {
        rigidBody.MovePosition(Vector2.MoveTowards(transform.position, (Vector2)target, movementSpeed * Time.deltaTime));
        InvokeWalkingEvents();
    }
    protected void InvokeWalkingEvents()
    {
        if (IsWalking)
        {
            ((ActorEvents)Events).OnWalking.Invoke();
        }
        else
        {
            ((ActorEvents)Events).OnIdle.Invoke();
        }
    }
}