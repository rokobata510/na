using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Items/Inventory")]
public sealed class Inventory : ScriptableObject
{
    private static Inventory instance;
    [SerializeField] private int gold;
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private int defaultMaxHealth;
    [SerializeField] private List<AWeapon> weapons;
    [SerializeField] private AWeapon equippedWeapon;
    [SerializeField] private AWeapon defaultWeapon;
    [SerializeField] private List<AItem> items;
    [SerializeField] private List<AItem> equippedItems;
    public InventoryEvents inventoryEvents = new();
    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            inventoryEvents.OnGoldChanged.Invoke();
        }
    }
    public List<AWeapon> Weapons
    {
        get
        {
            return weapons;
        }
        set
        {
            weapons = value;
            inventoryEvents.OnWeaponChanged.Invoke();
        }
    }
    public AWeapon EquippedWeapon
    {
        get
        {
            return equippedWeapon;
        }
        set
        {
            equippedWeapon = value;
            inventoryEvents.OnEquippedWeaponChanged.Invoke();
        }
    }
    public List<AItem> Items
    {
        get
        {
            return items;
        }
        set
        {
            items = value;
            inventoryEvents.OnItemChanged.Invoke();
        }
    }
    public List<AItem> EquippedItems
    {
        get
        {
            return equippedItems;
        }

        set
        {
            equippedItems = value;
            inventoryEvents.OnEquippedItemChanged.Invoke();
        }
    }
    public static Inventory Instance
    {
        get
        {
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            inventoryEvents.OnHealthChanged.Invoke();
        }
    }

    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }
        set
        {
            maxHealth = value;
            inventoryEvents.OnMaxHealthChanged.Invoke();
        }
    }

    public float CurrentHealthPercentage => 100 * Health / MaxHealth;

    public void Awake()
    {
        instance = instance != null ? instance : this;
        weapons ??= new List<AWeapon>();
        items ??= new List<AItem>();
        equippedItems ??= new List<AItem>();
    }

    public static void Reset()
    {
        instance.Health = instance.defaultMaxHealth;
        instance.MaxHealth = instance.defaultMaxHealth;
        instance.Gold = 0;
        instance.Weapons = new List<AWeapon> { instance.defaultWeapon };
        instance.EquippedWeapon = instance.defaultWeapon;
        instance.Items = new List<AItem>();
        instance.EquippedItems = new List<AItem>();
    }
}
