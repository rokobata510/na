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

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void CheckListLengths()
    {
        RoundRobinMultiWeaponRenderer[] renderers = GameObject.FindObjectsOfType<RoundRobinMultiWeaponRenderer>();

        foreach (var renderer in renderers)
        {
            if (renderer.weaponRenderers.Count != renderer.sprites.Count)
            {
                Debug.LogError($"RoundRobinMultiWeaponRenderer: The length of weaponRenderers ({renderer.weaponRenderers.Count}) does not match the length of sprites ({renderer.sprites.Count}).");
                throw new System.Exception("RoundRobinMultiWeaponRenderer: The length of weaponRenderers does not match the length of sprites.");
            }
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