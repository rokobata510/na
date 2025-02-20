using System.Collections;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]

public abstract class AAttackable : MonoBehaviourWithYLevelHandler
{
    public bool invincible;
    public float invincibilityTime;
    public int health;
    public Rigidbody2D rigidBody;
    protected SpriteRenderer spriteRenderer;


    public abstract AttackableEvents Events { get; set; }


    public void GetAttacked(GameObject attacker, IDealsDamage attackerProps) 
    {
        if (invincible)
        {
            return;
        }
        health -= attackerProps.Damage;
        Events.OnDamaged.Invoke(attacker, attackerProps);
        if (health <= 0)
        {
            Events.OnDeath.Invoke();
        }
        else
        {
            if (!attackerProps.GivesInvincibilityFrames)
            {
                return;
            }
            StartCoroutine(TriggerInvincibility());
        }
    }
    public virtual void Die()
    {
        Destroy(gameObject);
    }

    protected IEnumerator TriggerInvincibility()
    {
        invincible = true;
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(invincibilityTime);
        spriteRenderer.color = originalColor;
        invincible = false;
    }

}
