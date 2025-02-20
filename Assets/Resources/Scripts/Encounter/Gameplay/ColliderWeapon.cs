public class ColliderWeapon : AWeapon
{
    public new ColliderWeaponAttacker WeaponAttacker { get => (ColliderWeaponAttacker)weaponAttackerClone; set=> weaponAttackerClone = value; }
}
