using UnityEngine;

[CreateAssetMenu(fileName = "WeaponContainingItem", menuName = "Items/WeaponContainingItem")]
public class WeaponContainingItem : AItem
{
    public AWeapon weapon;

    public void OnEnable()
    {
        sprite = weapon.gameObject.GetComponent<SpriteRenderer>().sprite;
    }
    public override void AddToInventory()
    {
        Inventory.Instance.Weapons.Add(weapon);
    }
}

