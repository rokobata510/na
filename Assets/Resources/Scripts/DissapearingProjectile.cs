public class DissapearingProjectile : AProjectile
{
    protected override void WallInteraction(NormalizedVector3 collisionNormal)
    {
        Destroy(gameObject);
    }
}

