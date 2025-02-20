using System;
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(CapsuleCollider2D))]
public abstract class AProjectile : MonoBehaviourWithYLevelHandler, IDealsDamage
{
    private float speed;
    public float lifetimeInSeconds;
    private CapsuleCollider2D capsuleCollider;
    public Vector2 offset;
    private readonly float ThresholdAtWhichColliderGetsBiggerToAvoidTunneling = 3f;

    public float Speed { protected get => speed; set => speed = value; }
    protected int damage;
    public int Damage { get => (int)damage; set => damage = value; }

    private float knockback;
    public float Knockback => knockback;

    public bool GivesInvincibilityFrames => true;

    public void LoadProps(ProjectileSpawningProps props)
    {
        Speed = props.speed;
        lifetimeInSeconds = props.lifetime;
        Damage = props.damage;
        gameObject.layer = props.layer;
        gameObject.tag = props.tag;
    }

    public virtual void Fire(NormalizedVector3 direction)
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        if (Speed >= ThresholdAtWhichColliderGetsBiggerToAvoidTunneling)
        {
            float vericalSize = (speed - 3) / 5;
            float verticalOffset = -0.5f * (vericalSize - capsuleCollider.size.y);
            capsuleCollider.size = new Vector2(capsuleCollider.size.x, vericalSize);
            capsuleCollider.offset = new Vector2(0f, verticalOffset);
        }

        StartCoroutine(FireCoroutine(direction));
        transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(direction.Y, direction.X) * Mathf.Rad2Deg) - 90f);
        transform.position += transform.rotation * offset;
    }
    public virtual IEnumerator FireCoroutine(NormalizedVector3 direction)
    {
        capsuleCollider.enabled = true;
        float i = lifetimeInSeconds;

        while (i >= 0f)
        {
            transform.position += direction * Speed * Time.deltaTime;
            i -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != gameObject.layer)
        {
            TryToDealDamage(collision.gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            WallInteraction();
        }

    }

    public void TryToDealDamage(GameObject target)
    {
        if (target.TryGetComponent(out AAttackable attackable))
        {
            attackable.GetAttacked(gameObject, this);
            Destroy(gameObject);
        }
    }

    protected abstract void WallInteraction();
}
