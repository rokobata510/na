using System.Collections;
using UnityEngine;

public sealed class PlayerScript : AActor
{
    [SerializeField] PlayerMovementStrategy movementStrategy;
    public override AMovementStrategy MovementStrategy { get => movementStrategy; set => movementStrategy = (PlayerMovementStrategy)value; }
    PlayerMovementStrategy movementStrategyInstance;
    public override AMovementStrategy MovementStrategyInstance { get => movementStrategyInstance; set => movementStrategyInstance = (PlayerMovementStrategy)value; }
    public float rollSpeedMultiplier = 1f;
    public float rollTime = 0.6f;
    public float rollCooldown = 2f;
    private float timeOfLastRoll = 0;
    public GameObject deathScreen;
    private InputActions inputActions;


    [SerializeField] PlayerEvents events = new();
    public override AttackableEvents Events { get => events; set => events = (PlayerEvents)value; }

    public override void Awake()
    {
        base.Awake();
        health = Inventory.Instance.Health;
        ((PlayerMovementStrategy)MovementStrategyInstance).OnEnable();
        ((PlayerAttackStrategy)attackStrategyInstance).OnEnable();
        inputActions = new InputActions();
        inputActions.PlayerActions.Enable();

        events.OnDamaged.AddListener((GameObject attackergameObject, IDealsDamage attackerProps) => Inventory.Instance.Health = health);
        ((ActorEvents)Events).OnIdle.AddListener(() => animator.SetBool("isWalking", false));
        ((ActorEvents)Events).OnWalking.AddListener(() => animator.SetBool("isWalking", true));
        events.OnRolling.AddListener(() => animator.SetBool("isRolling", true));
        events.OnIdle.AddListener(() => animator.SetBool("isWalking", false));
        events.OnNotRolling.AddListener(() => animator.SetBool("isRolling", false));
        events.OnDeath.AddListener(() => Die());

    }

    public void FixedUpdate()
    {
        if (health <= 0)
        {
            events.OnDeath.Invoke();
            return;
        }
        GetNextStepTarget();
        MoveToTarget();
        FlipSpriteIfNeeded();
        RollIfNeeded();



        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();

        }
        attackStrategyInstance.SetTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        AttackLogic();
        weaponScript.Point((UnnormalizedVector3)transform.position, attackStrategyInstance.targetDirection);
        void RollIfNeeded()
        {
            if (!movementStrategyInstance.isRolling && IsWalking && Input.GetMouseButton(1) && NextRollAvailable())
            {
                StartCoroutine(Roll((NormalizedVector3)target));
            }
        }


    }

    public void OnDisable()
    {
        Debug.Log("OnDisable");
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
        deathScreen.SetActive(true);
        Inventory.Reset();
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
    }
}
