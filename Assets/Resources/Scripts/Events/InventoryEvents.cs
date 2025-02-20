using UnityEngine.Events;

public class InventoryEvents
{
    public UnityEvent OnGoldChanged = new();
    public UnityEvent OnHealthChanged = new();
    public UnityEvent OnMaxHealthChanged = new();
    public UnityEvent OnWeaponChanged = new();
    public UnityEvent OnItemChanged = new();
    public UnityEvent OnEquippedItemChanged = new();
    public UnityEvent OnEquippedWeaponChanged = new();
}

