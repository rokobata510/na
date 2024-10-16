public class ColliderWeapon : AWeapon
{
    public float maxTicksPerSecond;
    //TODO: on start állítsd be a collidert, és a méretét
    public override void Attack(NormalizedVector3 direction)
    {
        throw new System.NotImplementedException();
    }

    public override void Point(UnnormalizedVector3 positionOfOwner, NormalizedVector3 direction)
    {
        return;
    }
}

