using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;
public class MeleeWeapon : AProjectileWeapon
{
    public new MeleeWeaponRenderer WeaponRenderer { get => (MeleeWeaponRenderer)weaponRendererClone; set => weaponRendererClone = value; }
    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override void Attack(GameObject userGameObject, UnnormalizedVector3 targetPosition)
    {
        base.Attack(userGameObject, targetPosition);
        if (attackedThisFrame)
            WeaponRenderer.weaponOnTheLeft = !WeaponRenderer.weaponOnTheLeft;
    }
    public override void DontAttack(GameObject userGameObject)
    {
        base.DontAttack(userGameObject);
        if (attackedThisFrame)
            WeaponRenderer.weaponOnTheLeft = !WeaponRenderer.weaponOnTheLeft;
    }
}
