
using UnityEngine;

public interface IDealsDamage
{
    public int Damage { get; set; }
    public float Knockback { get; }
    public bool GivesInvincibilityFrames { get; }
    public void TryToDealDamage(GameObject DealDamageToThisTarget);

}

