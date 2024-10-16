using System;
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(CapsuleCollider2D))]
public abstract class AProjectile : ADealsDamage
{
    private float speed;
    private float lifetimeInSeconds;
    private CapsuleCollider2D capsuleCollider;
    public Vector2 offset;
    private readonly float ThresholdAtWhichColliderGetsBiggerToAvoidTunneling = 3f;

    public float Speed {protected get => speed; set => speed = value; }
    public float LifetimeInSeconds {protected get => lifetimeInSeconds; set => lifetimeInSeconds = value; }

    public virtual void Fire(NormalizedVector3 direction)
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        if (Speed >= ThresholdAtWhichColliderGetsBiggerToAvoidTunneling)
        {
            float vericalSize = (speed - 3) / 5;
            float verticalOffset = -0.5f*(vericalSize - capsuleCollider.size.y);
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
        float i = LifetimeInSeconds;

        while (i >= 0f)
        {
            transform.position += direction * Speed * Time.deltaTime;
            i -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(gameObject.tag))
        {
            TryToDealDamage(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Walls"))
        {
            WallInteraction(collision.GetContact(0).normal);
        }

    }

    protected virtual void WallInteraction(NormalizedVector3 collisionNormal)
    {
    }
}
