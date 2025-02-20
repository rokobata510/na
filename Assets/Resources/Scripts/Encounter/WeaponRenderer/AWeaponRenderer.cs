using UnityEngine;

public abstract class AWeaponRenderer : ClonableScriptableObject
{
    public float distanceFromUser;
    public float forwardsOffset;
    public float rotationOffset;
    public float LerpSpeed;

    public abstract UnnormalizedVector3 CalculateGoalPosition(UnnormalizedVector3 ownerPosition, Transform weaponTransform, NormalizedVector3 pointingDirection);
    public abstract Quaternion CalculateGoalRotation(NormalizedVector3 pointingDirection);
    public virtual void Snap(UnnormalizedVector3 ownerPosition, Transform weaponTransform, NormalizedVector3 pointingDirection)
    {
        UnnormalizedVector3 newPosition = CalculateGoalPosition(ownerPosition, weaponTransform, pointingDirection);
        Quaternion newRotation = CalculateGoalRotation(pointingDirection);
        weaponTransform.SetPositionAndRotation(newPosition, newRotation);
    }

    public virtual void Move(UnnormalizedVector3 ownerPosition, Transform weaponTransform, NormalizedVector3 pointingDirection)
    {
        UnnormalizedVector3 newPosition = CalculateGoalPosition(ownerPosition, weaponTransform, pointingDirection);
        Quaternion newRotation = CalculateGoalRotation(pointingDirection);
        weaponTransform.SetPositionAndRotation(Lerp(weaponTransform.position, newPosition, LerpSpeed), Quaternion.Lerp(weaponTransform.rotation, newRotation, LerpSpeed));
    }
    public UnnormalizedVector3 Lerp(UnnormalizedVector3 originalPosition, UnnormalizedVector3 newPosition, float LerpSpeed)
    {
        return Vector3.Lerp(originalPosition, newPosition, LerpSpeed * Time.deltaTime);
    }

}