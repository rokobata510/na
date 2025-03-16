using UnityEngine;

public static class SightChecker
{
    public static bool CanSeeTarget(UnnormalizedVector3 origin, NormalizedVector3 targetdirection, float sightRange, LayerMask layerMask)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, targetdirection, sightRange, layerMask);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Walls"))
                {
                    Debug.DrawRay(origin, targetdirection * sightRange, Color.magenta);
                    return false;
                }
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    Debug.DrawRay(origin, targetdirection * sightRange, Color.white);
                    return true;
                }
            }
        }
        return false;
    }
    public static bool CanSeeTarget(UnnormalizedVector3 origin, NormalizedVector3 targetDirection, float sightRange) => CanSeeTarget(origin, targetDirection, sightRange, LayerMask.GetMask("Player", "Walls"));

    internal static bool CanSeeTarget(UnnormalizedVector3 origin, UnnormalizedVector3 targetPosition, float sightRange) => CanSeeTarget(origin, (NormalizedVector3)(targetPosition - origin), sightRange);
}

