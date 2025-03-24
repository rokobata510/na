using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoundRobinMultiWeaponRenderer", menuName = "WeaponRenderers/RoundRobinMultiWeaponRenderer")]
public class RoundRobinMultiWeaponRenderer : AWeaponRenderer
{
    public List<AWeaponRenderer> weaponRenderers;
    public List<Sprite> sprites;
    private int currentIndex = 1;

    public int CurrentIndex
    {
        get => currentIndex;
        set
        {
            if (value < 0 || value >= weaponRenderers.Count)
            {
                throw new System.ArgumentOutOfRangeException(nameof(value), "Index is out of bounds of the weaponRenderers list.");
            }
            currentIndex = value;
        }
    }

    public override UnnormalizedVector3 CalculateGoalPosition(UnnormalizedVector3 ownerPosition, Transform weaponTransform, NormalizedVector3 pointingDirection)
    {
        return weaponRenderers[currentIndex].CalculateGoalPosition(ownerPosition, weaponTransform, pointingDirection);
    }

    public override Quaternion CalculateGoalRotation(NormalizedVector3 pointingDirection)
    {
        return weaponRenderers[currentIndex].CalculateGoalRotation(pointingDirection);
    }

    public override void Snap(UnnormalizedVector3 ownerPosition, Transform weaponTransform, NormalizedVector3 pointingDirection)
    {
        base.Snap(ownerPosition, weaponTransform, pointingDirection);
        weaponTransform.GetComponent<SpriteRenderer>().sprite = sprites[currentIndex];
        currentIndex = (currentIndex + 1) % weaponRenderers.Count;
    }
}