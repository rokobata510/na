
using UnityEngine;
public class InventoryTestFixture: MonoBehaviour
{
    [SerializeField] public Inventory testInventoryInstance;

    public void Awake()
    {
        Inventory.Instance = testInventoryInstance;
    }
}

