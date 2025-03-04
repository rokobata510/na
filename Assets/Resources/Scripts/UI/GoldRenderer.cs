using TMPro;
using UnityEngine;

public class GoldRenderer : MonoBehaviour
{
    public void Start()
    {
        Inventory.Instance.inventoryEvents.OnGoldChanged.AddListener(UpdateGoldDisplay);
        Inventory.Instance.inventoryEvents.OnGoldChanged.Invoke();
    }
    public void UpdateGoldDisplay()
    {
        gameObject.GetComponent<TMP_Text>().text = "Gold : " + Inventory.Instance.Gold.ToString();
    }
}
