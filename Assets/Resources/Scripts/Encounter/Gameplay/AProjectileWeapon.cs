public class AProjectileWeapon : AWeapon
{
    public new AProjectileWeaponAttacker WeaponAttacker { get => (AProjectileWeaponAttacker)weaponAttackerClone; set => weaponAttackerClone = value; }

    public override void OnEnable()
    {
        base.OnEnable();
    }
}
