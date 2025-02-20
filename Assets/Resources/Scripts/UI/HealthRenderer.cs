using TMPro;
using UnityEngine;

public class HealthRenderer : MonoBehaviour
{
    public void Start()
    {

        Inventory.Instance.inventoryEvents.OnHealthChanged.AddListener(UpdateHealthDisplay);
        Inventory.Instance.inventoryEvents.OnHealthChanged.Invoke();
    }
    public void UpdateHealthDisplay()
    {
        gameObject.GetComponent<TMP_Text>().text = "Health : " + Inventory.Instance.Health.ToString();
    }
}
