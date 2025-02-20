using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndChestRewardRenderer : AGridPanelRenderer
{
    private AItem[] items;
    private float chestGold;

    public void SetChestData(float gold)
    {
        chestGold = gold;
    }

    protected override void OnPanelHidden()
    {
        ClearSlots();
    }

    protected override void OnPanelShown()
    {
        Time.timeScale = 0;
        int countOfSlots = Rows * Columns;
        items = new AItem[countOfSlots];
        for (int i = 0; i < countOfSlots; i++)
        {
            items[i] = RemainingItems.Instance.GetRandomItem();
        }
        PopulateRewardSlots();
    }

    private void PopulateRewardSlots()
    {
        for (int i = 0; i < items.Length; i++)
        {
            int x = i % Columns;
            int y = i / Columns;

            if (x >= Columns || y >= Rows || items[i] == null) continue;

            GameObject slot = slots[x, y];
            SetupRewardSlot(slot, items[i]);
        }
    }

    private void SetupRewardSlot(GameObject slot, AItem item)
    {
        foreach (Transform child in slot.transform)
        {
            Destroy(child.gameObject);
        }
        GameObject itemDisplay = new GameObject("ItemDisplay");
        Image itemImage = itemDisplay.AddComponent<Image>();
        itemImage.sprite = item.sprite;
        itemImage.transform.SetParent(slot.transform, false);
        itemImage.rectTransform.sizeDelta = new Vector2(slotWidth, slotHeight);

        Button slotButton = slot.GetComponent<Button>();
        slotButton.enabled = true;
        slotButton.onClick.RemoveAllListeners();
        slotButton.onClick.AddListener(() => OnRewardSelected(item));
    }

    private void OnRewardSelected(AItem selectedItem)
    {
        Time.timeScale = 1;

        Inventory.Instance.Gold += (int)chestGold;
        selectedItem.AddToInventory();
        Debug.Log($"Added {selectedItem.name} and {chestGold} gold to inventory");

        EnableMapObjects();
        SceneManager.LoadScene("Map");

        ToggleVisibility(false);
    }

    private void EnableMapObjects()
    {
        GameObject map = GameObject.Find("Map");
        foreach (Transform child in map.transform)
        {
            if (child.TryGetComponent(out Renderer renderer))
            {
                renderer.enabled = true;
            }
            if (child.TryGetComponent(out Collider2D collider))
            {
                collider.enabled = true;
            }
        }
    }

    private void ClearSlots()
    {
        foreach (GameObject slot in slots)
        {
            foreach (Transform child in slot.transform)
            {
                Destroy(child.gameObject);
            }
            slot.GetComponent<Button>().enabled = false;
        }
    }
}