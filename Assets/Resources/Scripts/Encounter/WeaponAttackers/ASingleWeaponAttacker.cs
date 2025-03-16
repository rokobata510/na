public abstract class ASingleWeaponAttacker : AProjectileWeaponAttacker
{
    public int damage;
    public float fireRate;
    public AProjectile projectile;
    public override float FireRate { get => fireRate; set => fireRate = value; }
    public override AProjectile Projectile { get => projectile; set => projectile = value; }
    public override int Damage { get => damage; set => damage = value; }

    protected bool CooledDown(float currentTime)
    {
        return currentTime > (timeOfLastAttack + (1 / FireRate));
    }

}

