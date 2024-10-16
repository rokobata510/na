using System;
using UnityEngine;

public  class RangedWeapon : AProjectileWeapon
{
    public override void Point(UnnormalizedVector3 positionOfOwner, NormalizedVector3 target)
    {
        float angle = Mathf.Atan2(target.Y, target.X) * Mathf.Rad2Deg;
        Quaternion newRotation;
        if (Math.Abs(angle) > 90)
            newRotation = Quaternion.Euler(180, 0, -angle - rotationOffset * 360);
        else
            newRotation = Quaternion.Euler(0, 0, angle - rotationOffset * 360);
        UnnormalizedVector3 newPosition =new(
            positionOfOwner +
            (Quaternion.Euler(0, 0, angle) * Vector3.right) +
            transform.rotation * new Vector3(1, 1, 0) * distanceFromUser);
        transform.SetPositionAndRotation(newPosition, newRotation);

    }
}

