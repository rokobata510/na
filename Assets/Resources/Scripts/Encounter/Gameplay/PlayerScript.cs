using System.Collections;
using TMPro;
using UnityEngine;

public sealed class PlayerScript : AActor
{
    [SerializeField] PlayerMovementStrategy movementStrategy;
    public override AMovementStrategy MovementStrategy { get => movementStrategy; set => movementStrategy = (PlayerMovementStrategy)value; }
    PlayerMovementStrategy movementStrategyInstance;
    public override AMovementStrategy MovementStrategyInstance { get => movementStrategyInstance; set => movementStrategyInstance = (PlayerMovementStrategy)value; }
    TextMeshProUGUI HealthDisplay;
    public float rollSpeedMultiplier = 1f;
    public float rollTime = 0.6f;
    public float rollCooldown = 2f;
    private float timeOfLastRoll = 0;



    [SerializeField] PlayerEvents events = new();
    public override AttackableEvents Events { get => events; set => events = (PlayerEvents)value; }

    public override void Awake()
    {
        base.Awake();
        health = Inventory.Instance.Health;
        events.OnDamaged.AddListener((GameObject attackergameObject, IDealsDamage attackerProps) => Inventory.Instance.Health = health);
        events.OnWalking.AddListener(() => animator.SetBool("isWalking", true));
        events.OnRolling.AddListener(() => animator.SetBool("isRolling", true));
        events.OnIdle.AddListener(() => animator.SetBool("isWalking", false));
        events.OnNotRolling.AddListener(() => animator.SetBool("isRolling", false));

    }

    public void FixedUpdate()
    {
        GetNextStepTarget();
        MoveToTarget();
        FlipSpriteIfNeeded();
        RollIfNeeded();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();

        }
        void RollIfNeeded()
        {
            if (!movementStrategyInstance.isRolling && IsWalking && Input.GetMouseButton(1) && NextRollAvailable())
            {
                StartCoroutine(Roll((NormalizedVector3)target));
            }
        }


    }
    public new void Update()
    {
        base.Update();
        attackStrategyInstance.SetTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        AttackLogic();
    }


    private void Interact()
    {

        Collider2D[] colliders = Physics2D.OverlapAreaAll(transform.position - new Vector3(2, 2), transform.position + new Vector3(2, 2));
        Debug.DrawLine(transform.position - new Vector3(2, 2), transform.position + new Vector3(2, 2), Color.red, 2);
        IInteractable closestInteractable = null;
        float closestDistance = float.MaxValue;
        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out IInteractable interactable))
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestInteractable = interactable;
                }
            }
        }
        closestInteractable?.Interact();
    }

    private IEnumerator Roll(NormalizedVector3 direction)
    {
        events.OnRolling.Invoke();
        movementStrategyInstance.isRolling = true;
        invincible = true;
        timeOfLastRoll = Time.time;
        gameObject.layer = LayerMask.NameToLayer("Rolling");
        float timePassed = 0;
        while (timePassed <= rollTime)
        {
            timePassed += Time.deltaTime;
            rigidBody.MovePosition(Vector2.MoveTowards(transform.position, (Vector2)(transform.position + direction), MovementSpeed * Time.deltaTime * rollSpeedMultiplier));
            yield return null;
        }
        events.OnNotRolling.Invoke();
        movementStrategyInstance.isRolling = false;
        invincible = false;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
    bool NextRollAvailable()
    {
        return Time.time > timeOfLastRoll + rollCooldown;
    }


    public override void Die()
    {
        throw new System.NotImplementedException();
    }
}
