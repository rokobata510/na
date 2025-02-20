public class DissapearingProjectile : AProjectile
{
    protected override void WallInteraction()
    {
        Destroy(gameObject);
    }
}

