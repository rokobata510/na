using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlotData : MonoBehaviour
{
    public AItem item;
}

public class ShopRenderer : AGridPanelRenderer
{
    public int costTextWidth;
    public int costTextHeight;
    public GameObject costTextPrefab;
    protected GameObject closeShopButton;
    private List<AItem> itemsInShop = new();
    int ItemsToGenerate => Rows * Columns;

    public override int PanelWidth => margin + Columns * (slotWidth + margin);
    public override int PanelHeight => margin + Rows * (slotHeight + 2 * margin + costTextHeight);

    public void Start()
    {
        closeShopButton = transform.Find("CloseShopButton").gameObject;
    }

    public void Update() { }

    protected override void OnPanelHidden()
    {
        ClearSlots();
        HideDescription();
    }

    private void ClearSlots()
    {
        itemsInShop.Clear();
        closeShopButton.SetActive(false);
        foreach (GameObject slot in slots)
        {
            Button slotButton = slot.GetComponent<Button>();
            slotButton.onClick.RemoveAllListeners();

            foreach (Transform child in slot.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    protected override void OnPanelShown()
    {
        PopulateShop();
    }

    private void PopulateShop()
    {
        closeShopButton.SetActive(true);
        for (int i = 0; i < ItemsToGenerate; i++)
        {
            AItem item = GetShopItem();
            int x = i % Columns;
            int y = i / Columns;

            if (x >= Columns || y >= Rows) continue;

            GameObject slot = slots[x, y];
            SetupShopSlot(slot, item);
        }
    }

    private AItem GetShopItem()
    {
        AItem itemThatWillBeInTheShop = RemainingItems.Instance.GetItemExceptTheListWithoutRemovingIt(itemsInShop);
        itemsInShop.Add(itemThatWillBeInTheShop);
        return itemThatWillBeInTheShop;
    }

    protected override void CreateSlots()
    {
        float startingX = -PanelWidth / 2 + margin + (slotWidth / 2);
        float startingY = PanelHeight / 2 - (margin + (slotHeight / 2));
        float xIncrement = slotWidth + margin;
        float yIncrement = -(slotHeight + (2 * margin) + costTextHeight);

        for (int y = 0; y < Rows; y++)
        {
            for (int x = 0; x < Columns; x++)
            {
                GameObject slot = Instantiate(slotPrefab, transform);
                slot.transform.localPosition = new Vector3(
                    startingX + x * xIncrement,
                    startingY + y * yIncrement,
                    0
                );
                slot.GetComponent<RectTransform>().sizeDelta = new Vector2(slotWidth, slotHeight);
                slots[x, y] = slot;
                InitializeSlot(slot);
            }
        }
    }

    private void SetupShopSlot(GameObject slot, AItem item)
    {
        Image slotImage = slot.GetComponent<Image>();
        Button slotButton = slot.GetComponent<Button>();

        GameObject itemDisplay = new GameObject("ShopDisplay");
        Image itemImage = itemDisplay.AddComponent<Image>();
        itemImage.material = slotImage.material;
        itemImage.sprite = item.sprite;
        itemImage.transform.SetParent(slot.transform);
        itemImage.rectTransform.sizeDelta = new Vector2(slotWidth, slotHeight);
        itemImage.rectTransform.localPosition = Vector3.zero;
        slotButton.enabled = true;

        GameObject costText = Instantiate(costTextPrefab, slot.transform);
        costText.transform.position = new Vector3(
            slot.transform.position.x,
            slot.transform.position.y - slotHeight / 2 - margin - costTextHeight / 2,
            0
        );
        costText.GetComponent<TextMeshProUGUI>().text = item.cost.ToString();
        costText.GetComponent<RectTransform>().sizeDelta = new Vector2(costTextWidth, costTextHeight);
        slotButton.onClick.AddListener(() => BuyItem(item, slot));

        ShopSlotData slotData = slot.AddComponent<ShopSlotData>();
        slotData.item = item;
    }

    protected override string GetSlotName(GameObject slot)
    {
        var data = slot.GetComponent<ShopSlotData>();
        return data?.item?.name ?? "";
    }

    protected override string GetSlotDescription(GameObject slot)
    {
        var data = slot.GetComponent<ShopSlotData>();
        return data?.item?.description ?? "";
    }

    public void HideShop()
    {
        ToggleVisibility(false);
    }

    private void BuyItem(AItem item, GameObject slot)
    {
        if (Inventory.Instance.Gold >= item.cost)
        {
            Inventory.Instance.Gold -= item.cost;
            Inventory.Instance.Items.Add(RemainingItems.Instance.GetItem(item));
            slot.GetComponentInChildren<TextMeshProUGUI>().text = "";
            slot.GetComponent<Button>().enabled = false;
            Destroy(slot.transform.GetChild(0).GetComponent<Image>());
        }
    }
}