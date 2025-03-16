public class RangedWeapon : AProjectileWeapon
{

    public new RangedWeaponRenderer WeaponRenderer { get => (RangedWeaponRenderer)weaponRendererClone; set => weaponRendererClone = value; }
    public override void OnEnable()
    {
        base.OnEnable();
    }
}

