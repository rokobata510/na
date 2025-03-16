using UnityEngine;
using UnityEngine.UI;

public class InventoryRenderer : AGridPanelRenderer
{
    public Color backDropColor;
    public Color selectedItemColor;
    public override int PanelWidth => margin + Columns * (slotWidth + margin);
    public override int PanelHeight => margin + Rows * (slotHeight + margin);

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void InitializeSlot(GameObject slot)
    {
        base.InitializeSlot(slot);
        slot.GetComponent<Image>().color = backDropColor;
    }

    private void OnGUI()
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

        GameObject itemDisplay = new("ItemDisplay");
        Image itemImage = itemDisplay.AddComponent<Image>();
        itemImage.sprite = item.sprite;
        itemImage.transform.SetParent(slot.transform);
        itemImage.rectTransform.sizeDelta = new Vector2(slotWidth, slotHeight);
        itemImage.rectTransform.localPosition = Vector3.zero;
        slotButton.enabled = true;
        slotButton.onClick.AddListener(() => ToggleItemEquip(item, slot));
        UpdateSlotColor(item, slot);

        InventorySlotData slotData = slot.AddComponent<InventorySlotData>();
        slotData.item = item;
    }

    private void ToggleItemEquip(AItem item, GameObject slot)
    {
        if (Inventory.Instance.EquippedItems.Contains(item))
        {
            Inventory.Instance.EquippedItems.Remove(item);
        }
        else
        {
            Inventory.Instance.EquippedItems.Add(item);
        }
        UpdateSlotColor(item, slot);
    }

    private void UpdateSlotColor(AItem item, GameObject slot)
    {
        slot.GetComponent<Image>().color = Inventory.Instance.EquippedItems.Contains(item)
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

    protected override string GetSlotName(GameObject slot)
    {
        var data = slot.GetComponent<InventorySlotData>();
        return data?.item?.name ?? "";
    }

    protected override string GetSlotDescription(GameObject slot)
    {
        var data = slot.GetComponent<InventorySlotData>();
        return data?.item?.description ?? "";
    }
}

public class InventorySlotData : MonoBehaviour
{
    public AItem item;
}