using System;
using System.Collections;
using TMPro;
using UnityEngine;

public sealed class PlayerScript : AActor
{
    [SerializeField] PlayerMovementStrategy movementStrategy;
    public override AMovementStrategy MovementStrategy { get => movementStrategy; set => movementStrategy = (PlayerMovementStrategy)value; }
    PlayerMovementStrategy movementStrategyInstance;
    protected override AMovementStrategy MovementStrategyInstance { get => movementStrategyInstance; set => movementStrategyInstance = (PlayerMovementStrategy)value; }
    TextMeshProUGUI HealthDisplay;
    public float rollSpeedMultiplier = 1f;
    public float rollTime = 0.6f;
    [SerializeField] PlayerEvents events = new();
    public override AttackableEvents Events { get => events; set =>events =(PlayerEvents)value; }

    public override void Start()
    {
        base.Start();
        //TODO: ne string alapján keresd meg a health displayt
        HealthDisplay = GameObject.Find("Player Health Display").GetComponent<TextMeshProUGUI>();
        HealthDisplay.text = "Health: " + health;
        events.OnDamaged.AddListener((ADealsDamage attacker) => UpdateHealthDisplay());
        events.OnWalking.AddListener(() => animator.SetBool("isWalking", true));
        events.OnRolling.AddListener(() => animator.SetBool("isRolling", true));
        events.OnIdle.AddListener(() => animator.SetBool("isWalking", false));
        events.OnPlayerNotRolling.AddListener(() => animator.SetBool("isRolling", false));

    }

    private void UpdateHealthDisplay()
    {
        HealthDisplay.text = "Health: " + health;
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
        AttackLogic();
        void RollIfNeeded()
        {
            if (!movementStrategyInstance.isRolling && IsWalking && Input.GetMouseButton(1))
            {
                StartCoroutine(Roll((NormalizedVector3)target));
            }
        }

        
    }

    private void Interact()
    {
        Collider2D[] colliders = Physics2D.OverlapAreaAll(transform.position - new Vector3(1, 1), transform.position + new Vector3(1, 1));
        Debug.DrawLine(transform.position - new Vector3(1, 1), transform.position + new Vector3(1, 1), Color.red, 1);
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
        if (closestInteractable != null)
        {
            closestInteractable.Interact();
        }
    }

    private IEnumerator Roll(NormalizedVector3 direction)
    {
        events.OnRolling.Invoke();
        movementStrategyInstance.isRolling = true;
        invincible = true;
        gameObject.layer = LayerMask.NameToLayer("Rolling");
        float timePassed = 0;
        while (timePassed <= rollTime)
        {
            timePassed += Time.deltaTime;
            rigidBody.MovePosition(Vector2.MoveTowards(transform.position, (Vector2)(transform.position + direction), movementSpeed * Time.deltaTime * rollSpeedMultiplier));
            yield return null;
        }
        events.OnPlayerNotRolling.Invoke();
        movementStrategyInstance.isRolling = false;
        invincible = false;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    

    public override void Die()
    {
        throw new System.NotImplementedException();
    }
}
