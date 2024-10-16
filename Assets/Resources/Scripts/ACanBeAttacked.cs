using System.Collections;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]

public abstract class ACanBeAttacked : MonoBehaviourWithYLevelHandler
{
    public bool invincible;
    public float invincibilityTime;
    public int health;
    public Rigidbody2D rigidBody;
    protected SpriteRenderer spriteRenderer;


    public abstract AttackableEvents Events { get; set; }
    public virtual void Start()
    {
        Events.OnDeath.AddListener(Die);
        Events.OnDamaged.AddListener((ADealsDamage attacker) => GetAttacked(attacker));
    }

    public void GetAttacked(ADealsDamage attacker) 
    {
        if (invincible)
        {
            return;
        }
        health -= attacker.Damage;
        if (health <= 0)
        {
            Events.OnDeath.Invoke();
        }
        else
        {
            StartCoroutine(TriggerInvincibility());
        }
    }
    /*public virtual IEnumerator GetKnockedBack(NormalizedVector3 direction, float force)
    {
        float i = knockbackTimeInSeconds;

        while (i > 0)
        {
            rigidBody.MovePosition((Vector3)rigidBody.position + force * Time.deltaTime * direction.Vector);
            i -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
    */
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
