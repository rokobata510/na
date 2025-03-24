
using UnityEngine;

public class EncounterInventoryInitializer: MonoBehaviour
{
    public Inventory inventory;

    public void Awake()
    {
        Inventory.Instance = inventory;

        Inventory.Instance.Awake();
    }
}

