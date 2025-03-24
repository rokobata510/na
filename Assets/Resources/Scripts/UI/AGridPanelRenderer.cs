using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
    public TMP_Text descriptionText;
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

        EventTrigger eventTrigger = slot.GetComponent<EventTrigger>();
        if (eventTrigger == null)
            eventTrigger = slot.AddComponent<EventTrigger>();

        EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
        pointerEnter.eventID = EventTriggerType.PointerEnter;
        pointerEnter.callback.AddListener((data) => { OnSlotHover(slot); });

        EventTrigger.Entry pointerExit = new EventTrigger.Entry();
        pointerExit.eventID = EventTriggerType.PointerExit;
        pointerExit.callback.AddListener((data) => { OnSlotHoverEnd(); });

        eventTrigger.triggers.Add(pointerEnter);
        eventTrigger.triggers.Add(pointerExit);
    }

    protected virtual void OnSlotHover(GameObject slot)
    {
        
        string name = GetSlotName(slot);
        string description = GetSlotDescription(slot);
        if (name != "" && description != "")
        {
            ShowDescription(name, description);
        }
    }

    protected virtual void OnSlotHoverEnd()
    {
        HideDescription();
    }

    protected virtual void ShowDescription(string name, string description)
    {
        if (descriptionText != null)
        {
            descriptionText.enabled = true;
            descriptionText.text = $"{name}: \n{description}";
        }
        GameObject.Find("HoverDescriptionImage").GetComponent<Image>().enabled = true;
    }

    protected virtual void HideDescription()
    {
        if (descriptionText != null)
        {
            descriptionText.enabled = false;
            descriptionText.text = "";
        }
        GameObject.Find("HoverDescriptionImage").GetComponent<Image>().enabled = false;
    }

    protected virtual string GetSlotName(GameObject slot) => "";
    protected virtual string GetSlotDescription(GameObject slot) => "";

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