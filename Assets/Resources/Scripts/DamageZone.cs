using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class DamageZone : ADealsDamage
{

    public void OnTriggerStay2D(Collider2D collision)
    {
        TryToDealDamage(collision.gameObject);
    }
}

