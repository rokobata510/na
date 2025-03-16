using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public abstract class AWeapon : MonoBehaviourWithYLevelHandler
{
    protected bool attackedThisFrame = false;
    public AWeaponRenderer weaponRenderer;

    public AWeaponAttacker weaponAttacker;

    protected AWeaponRenderer weaponRendererClone;
    protected AWeaponAttacker weaponAttackerClone;
    public virtual AWeaponRenderer WeaponRenderer { get => weaponRendererClone; set => weaponRendererClone = value; }
    public virtual AWeaponAttacker WeaponAttacker { get => weaponAttackerClone; set => weaponAttackerClone = value; }

    public virtual void OnEnable()
    {
        weaponAttackerClone = Instantiate(weaponAttacker);
        weaponRendererClone = Instantiate(weaponRenderer);

    }
    public virtual void Attack(GameObject userGameObject, UnnormalizedVector3 targetPosition)
    {
        WeaponAttacker.Attack(userGameObject, targetPosition, Time.time, gameObject.layer);
        SetAttackedThisFrame();
    }


    public virtual void DontAttack(GameObject userGameObject)
    {
        WeaponAttacker.DontAttack(userGameObject);
        SetAttackedThisFrame();
    }
    private void SetAttackedThisFrame() => attackedThisFrame = WeaponAttacker.timeOfLastAttack == Time.time;
    public virtual void Point(UnnormalizedVector3 ownerPosition, NormalizedVector3 direction)
    {
        if (attackedThisFrame)
        {
            WeaponRenderer.Snap(ownerPosition, transform, direction);
        }
        else
        {
            WeaponRenderer.Move(ownerPosition, transform, direction);
        }
    }
}

