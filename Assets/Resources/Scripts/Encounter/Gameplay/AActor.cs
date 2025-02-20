using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public abstract class AActor : AAttackable
{

    public GameObject weapon;
    public AWeapon weaponScript;
    public AAttackStrategy attackStrategy;
    public AAttackStrategy attackStrategyInstance;
    public float movementSpeed;
    public abstract AMovementStrategy MovementStrategy { get; set; }
    public abstract AMovementStrategy MovementStrategyInstance { get; set; }
    protected bool IsWalking => target != (UnnormalizedVector3)transform.position;

    public float MovementSpeed { get => Mathf.Max(movementSpeed, 0.001f); set => movementSpeed = Mathf.Max(value, 0.001f); }

    protected Animator animator;
    public UnnormalizedVector3 target = new();


    public virtual void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        attackStrategyInstance = attackStrategy.Clone();
        MovementStrategyInstance = MovementStrategy.Clone();
        ((ActorEvents)Events).OnIdle.AddListener(() => animator.SetBool("isWalking", false));
        ((ActorEvents)Events).OnWalking.AddListener(() => animator.SetBool("isWalking", true));
        ((ActorEvents)Events).OnWeaponChange.AddListener(() => EquipWeapon());
    }

    private void EquipWeapon()
    {
        GameObject weaponGameObject = InstantiateWeapon(weapon);
        weaponScript = weaponGameObject.GetComponent<AWeapon>();
    }

    protected virtual void AttackLogic()
    {
        attackStrategyInstance.SetTarget(origin: (UnnormalizedVector3)transform.position);
        weaponScript.Point((UnnormalizedVector3)transform.position, attackStrategyInstance.targetDirection);
        if (attackStrategyInstance.WantsToAttack((UnnormalizedVector3)transform.position))
        {
            weaponScript.Attack(gameObject, attackStrategyInstance.targetGameObject.transform.position);
        }
        else
        {
            weaponScript.DontAttack(gameObject);
        }
    }

    public GameObject InstantiateWeapon(GameObject weapon)
    {
        GameObject newWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        newWeapon.transform.lossyScale.Set(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);
        newWeapon.layer = gameObject.layer;
        return newWeapon;
    }

    public override void Die()
    {
        Events.OnDeath.Invoke();
        Destroy(weaponScript.gameObject);
        Destroy(gameObject);
    }
    protected void FlipSpriteIfNeeded()
    {
        if (target.X < transform.position.x)
            spriteRenderer.flipX = true;
        else if (target.X > transform.position.x)
            spriteRenderer.flipX = false;
    }
    protected virtual void GetNextStepTarget() => target = transform.position + MovementStrategyInstance.GetNextStep(gameObject);
    protected virtual void MoveToTarget()
    {
        rigidBody.MovePosition(Vector2.MoveTowards(transform.position, (Vector2)target, MovementSpeed * Time.deltaTime));
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