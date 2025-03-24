
using UnityEngine;
using UnityEngine.UI;

public class InventoryRenderer : MonoBehaviour
{
    public int itemsPerRow;
    public int itemsPerColumn;
    public bool ItemsAreRenderedAsSquares;
    public int itemWidth;
    public int itemHeight;
    public int Margin;
    public GameObject inventorySlot;
    public int InventoryWidth => Margin + itemsPerRow * (itemWidth + Margin);
    public int InventoryHeight => Margin + itemsPerColumn * (itemHeight + Margin);

    protected Image image;
    protected GameObject[,] slots;
    public Color backDropColor;
    public Color selectedItemColor;
    public void Awake()
    {
        image = gameObject.GetComponent<Image>();
        image.enabled = false;
        image.rectTransform.sizeDelta = new Vector2(InventoryWidth, InventoryHeight);
        slots = new GameObject[itemsPerRow, itemsPerColumn];
        CreateItemSlots();

    }

    private void CreateItemSlots()
    {
        float startingX = -InventoryWidth / 2 + Margin + (itemWidth / 2);
        float startingY = InventoryHeight / 2 - (Margin + (itemHeight / 2));
        float XIncrement = itemWidth + Margin;
        float YIncrement = -(itemHeight + Margin);
        int slotIndexX = 0;
        int slotIndexY = 0;

        for (float y = startingY; y > -InventoryHeight / 2; y += YIncrement)
        {
            for (float x = startingX; x < InventoryWidth / 2; x += XIncrement)
            {
                GameObject slotBackDrop = Instantiate(inventorySlot, transform);
                slotBackDrop.transform.localPosition = new Vector3(x, y, 0);
                slotBackDrop.GetComponent<RectTransform>().sizeDelta = new Vector2(itemWidth, itemHeight);
                Image backDropImage = slotBackDrop.GetComponent<Image>();
                backDropImage.color = backDropColor;
                backDropImage.enabled = false;
                slotBackDrop.GetComponent<Button>().enabled = false;
                slots[slotIndexX, slotIndexY] = slotBackDrop;
                slotIndexX++;
            }
            slotIndexX = 0;
            slotIndexY++;
        }
    }


    public void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Tab)
        {
            image.enabled = !image.enabled;

            ShowOrHideSlots(image.enabled);
            ShowOrHideItems(image.enabled);
        }
    }

    private void ShowOrHideItems(bool imageIsEnabled)
    {

        for (int itemIndex = 0; itemIndex < Inventory.Instance.Items.Count; itemIndex++)
        {
            AItem item = Inventory.Instance.Items[itemIndex];
            Debug.Log("Inventory contains: " + item.name);
            GameObject slot = slots[itemIndex % itemsPerRow, itemIndex / itemsPerRow];
            slot.GetComponent<Button>().enabled = imageIsEnabled;
            UnityEngine.Events.UnityAction OnSlotClicked = () =>
            {
                Debug.Log("Clicked object: " + slot.name);
                if (Inventory.Instance.EquippedItems.Contains(item))
                {
                    Debug.Log("Removing item: " + item.name);
                    Inventory.Instance.EquippedItems.Remove(item);
                }
                else
                {
                    Debug.Log("Adding item: " + item.name);
                    Inventory.Instance.EquippedItems.Add(item);
                }
                ColorInTheSlot(item, slot);
            };
            Button slotButton = slot.GetComponent<Button>();
            if (imageIsEnabled)
            {
                ColorInTheSlot(item, slot);
                GameObject filledSlot = Instantiate(slot, transform);
                filledSlot.GetComponent<Button>().enabled = false;
                Image filledSlotImage = filledSlot.GetComponent<Image>();
                filledSlotImage.rectTransform.sizeDelta = new Vector2(itemWidth, itemHeight);
                filledSlotImage.enabled = true;
                filledSlotImage.sprite = item.sprite;
                filledSlotImage.transform.SetParent(slot.transform);
                slotButton.interactable = true;
                slotButton.onClick.AddListener(OnSlotClicked);

            }
            else
            {
                slotButton.onClick.RemoveAllListeners();
                slot.GetComponent<Image>().color = backDropColor;
                foreach (Transform child in slot.transform)
                {
                    Destroy(child.gameObject);
                }

            }

        }

    }

    private void ColorInTheSlot(AItem item, GameObject slot)
    {
        if (Inventory.Instance.EquippedItems.Contains(item))
        {
            slot.GetComponent<Image>().color = selectedItemColor;
        }
        else
        {
            slot.GetComponent<Image>().color = backDropColor;
        }
    }

    private void ShowOrHideSlots(bool imageIsEnabled)
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Image>().enabled = imageIsEnabled;
            child.GetComponent<Button>().enabled = imageIsEnabled;
        }
    }
}


