using UnityEngine;
using UnityEngine.UI;

public class InventoryRenderer : AGridPanelRenderer
{
    [Header("Inventory Specific")]
    public Color backDropColor;
    public Color selectedItemColor;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void InitializeSlot(GameObject slot)
    {
        base.InitializeSlot(slot);
        slot.GetComponent<Image>().color = backDropColor;
    }

    private new void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Tab)
        {
            ToggleVisibility(!panelImage.enabled);
        }
    }

    protected override void OnPanelShown()
    {
        PopulateInventorySlots();
    }

    protected override void OnPanelHidden()
    {
        ClearSlots();
    }

    private void PopulateInventorySlots()
    {
        for (int i = 0; i < Inventory.Instance.Items.Count; i++)
        {
            AItem item = Inventory.Instance.Items[i];
            int x = i % Columns;
            int y = i / Columns;

            if (x >= Columns || y >= Rows) continue;

            GameObject slot = slots[x, y];
            SetupInventorySlot(slot, item);
        }
    }

    private void SetupInventorySlot(GameObject slot, AItem item)
    {
        Image slotImage = slot.GetComponent<Image>();
        Button slotButton = slot.GetComponent<Button>();

        // Create item display
        GameObject itemDisplay = new GameObject("ItemDisplay");
        Image itemImage = itemDisplay.AddComponent<Image>();
        itemImage.sprite = item.sprite;
        itemImage.transform.SetParent(slot.transform);
        itemImage.rectTransform.sizeDelta = new Vector2(slotWidth, slotHeight);

        // Setup button functionality
        slotButton.enabled = true;
        slotButton.onClick.AddListener(() => ToggleItemEquip(item, slotImage));

        UpdateSlotColor(item, slotImage);
    }

    private void ToggleItemEquip(AItem item, Image slotImage)
    {
        if (Inventory.Instance.EquippedItems.Contains(item))
        {
            Inventory.Instance.EquippedItems.Remove(item);
        }
        else
        {
            Inventory.Instance.EquippedItems.Add(item);
        }
        UpdateSlotColor(item, slotImage);
    }

    private void UpdateSlotColor(AItem item, Image slotImage)
    {
        slotImage.color = Inventory.Instance.EquippedItems.Contains(item)
            ? selectedItemColor
            : backDropColor;
    }

    private void ClearSlots()
    {
        foreach (GameObject slot in slots)
        {
            Button slotButton = slot.GetComponent<Button>();
            slotButton.onClick.RemoveAllListeners();
            slot.GetComponent<Image>().color = backDropColor;

            foreach (Transform child in slot.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}