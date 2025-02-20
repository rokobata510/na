
using UnityEngine;

[CreateAssetMenu(fileName = "NullWeaponRenderer", menuName = "WeaponRenderers/NullWeaponRenderer")]
public class NullWeaponRenderer : AWeaponRenderer
{
    private void Start()
    {


    }
    public override UnnormalizedVector3 CalculateGoalPosition(UnnormalizedVector3 ownerPosition, Transform weaponTransform, NormalizedVector3 pointingDirection)
    {
        weaponTransform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        return new UnnormalizedVector3(0, 0, 0);
    }

    public override Quaternion CalculateGoalRotation(NormalizedVector3 pointingDirection)
    {
        return Quaternion.identity;
    }
}

