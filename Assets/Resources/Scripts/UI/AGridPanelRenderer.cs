using UnityEngine;
using UnityEngine.UI;

public abstract class AGridPanelRenderer : MonoBehaviour
{
    public int Rows;
    public int Columns;
    public int slotWidth;
    public int slotHeight;
    public int margin;
    public GameObject slotPrefab;

    protected Image panelImage;
    protected GameObject[,] slots;

    public abstract int PanelWidth { get; }
    public abstract int PanelHeight { get; }

    protected virtual void Awake()
    {
        panelImage = GetComponent<Image>();
        panelImage.enabled = false;
        panelImage.rectTransform.sizeDelta = new Vector2(PanelWidth, PanelHeight);
        slots = new GameObject[Columns, Rows];
        CreateSlots();
    }

    protected virtual void CreateSlots()
    {
        float startingX = -PanelWidth / 2 + margin + (slotWidth / 2);
        float startingY = PanelHeight / 2 - (margin + (slotHeight / 2));
        float xIncrement = slotWidth + margin;
        float yIncrement = -(slotHeight + margin);

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

    protected virtual void InitializeSlot(GameObject slot)
    {
        Image slotImage = slot.GetComponent<Image>();
        slotImage.enabled = false;
        slot.GetComponent<Button>().enabled = false;
    }


    public void ToggleVisibility(bool visible)
    {
        panelImage.enabled = visible;
        UpdateSlotVisibility(visible);

        if (visible) OnPanelShown();
        else OnPanelHidden();
    }
    public void ToggleVisibility() => ToggleVisibility(!panelImage.enabled);
    protected void UpdateSlotVisibility(bool visible)
    {
        foreach (GameObject slot in slots)
        {
            slot.GetComponent<Image>().enabled = visible;
        }
    }

    protected abstract void OnPanelShown();
    protected abstract void OnPanelHidden();
}