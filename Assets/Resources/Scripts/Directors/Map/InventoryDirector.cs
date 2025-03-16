using TMPro;
using UnityEngine;
public class InventoryDirector : AMapDirector
{
    TMP_Dropdown WeaponPicker;
    public Inventory inventory;

    public void Awake()
    {
        Inventory.Instance = inventory;
        Inventory.Instance.Awake();
    }

    public override void SetupFields(MapDirectorContainer directorContainer)
    {
        if (Inventory.Instance == null)
        {
            Inventory.Instance = inventory;
        }
        WeaponPicker = GameObject.Find("WeaponPicker").GetComponent<TMP_Dropdown>();
    }

    public override void StartDirector()
    {
        WeaponPicker.ClearOptions();
        WeaponPicker.AddOptions(Inventory.Instance.Weapons.ConvertAll(weapon => weapon.name));
        Inventory.Instance.EquippedWeapon = Inventory.Instance.Weapons[0];
    }
    public void OnWeaponPickerChanged()
    {
        Inventory.Instance.EquippedWeapon = Inventory.Instance.Weapons[WeaponPicker.value];
    }

}

