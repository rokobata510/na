using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class BouncingProjectile : AProjectile
{
    Rigidbody2D rigidBody;
    public void OnEnable()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    public override IEnumerator FireCoroutine(NormalizedVector3 direction)
    {
        rigidBody.velocity = direction * Speed;
        yield return new WaitForSeconds(lifetimeInSeconds);
        Destroy(gameObject);
    }
    protected override void WallInteraction()
    {
        return;
    }
}

