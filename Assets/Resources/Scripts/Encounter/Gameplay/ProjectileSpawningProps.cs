public class ProjectileSpawningProps
{
    public AProjectile projectile;
    public UnnormalizedVector3 position;
    public NormalizedVector3 direction;
    public int damage;
    public float knockback;
    public float speed;
    public float lifetime;
    public string tag;
    public int layer;

    public ProjectileSpawningProps(AProjectile projectile, UnnormalizedVector3 position, NormalizedVector3 direction, int damage, float knockback, float speed, float lifetime, int layer)
    {
        this.projectile = projectile;
        this.position = position;
        this.direction = direction;
        this.damage = damage;
        this.knockback = knockback;
        this.speed = speed;
        this.lifetime = lifetime;
        this.tag = "Projectile";
        this.layer = layer;
    }

    public ProjectileSpawningProps(AProjectile projectile, UnnormalizedVector3 position, NormalizedVector3 direction, int damage, float knockback, float speed, float lifetime, string tag, int layer)
    {
        this.projectile = projectile;
        this.position = position;
        this.direction = direction;
        this.damage = damage;
        this.knockback = knockback;
        this.speed = speed;
        this.lifetime = lifetime;
        this.tag = tag;
        this.layer = layer;
    }
}
