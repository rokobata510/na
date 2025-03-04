using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "RemainingItems", menuName = "Items/RemainingItems")]
public class RemainingItems : ScriptableObject
{
    private static RemainingItems instance;

    public static RemainingItems Instance
    {
        get
        {
            if (instance == null)
            {
                instance = CreateInstance<RemainingItems>();
            }
            return instance;
        }
    }

    public List<AItem> items;
    private void OnEnable()
    {
        items = ListOfEveryItem.Instance.items;
    }

    public AItem GetItem(int index)
    {
        if (index < 0 || index >= items.Count)
        {
            return null;
        }
        AItem item = items[index];
        items.RemoveAt(index);
        return item;
    }
    public AItem GetItem(AItem item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            return item;
        }
        throw new System.Exception("Item not found in ReminingItems");
    }

    public AItem GetRandomItem()
    {
        if (items.Count == 0)
        {
            return null;
        }
        int index = EncounterRandomStream.Range(0, items.Count);
        AItem item = items[index];
        items.RemoveAt(index);
        return item;
    }

    public AItem GetItemWithoutRemovingIt(int index)
    {
        if (index < 0 || index >= items.Count)
        {
            return null;
        }
        return items[index];
    }
    public AItem GetRandomItemWithoutRemovingIt()
    {
        if (items.Count == 0)
        {
            return null;
        }
        int index = EncounterRandomStream.Range(0, items.Count);
        return items[index];
    }

    public AItem GetItemExceptTheListWithoutRemovingIt(List<AItem> excludedItems)
    {
        List<AItem> itemsToChooseFrom = new();
        itemsToChooseFrom = items.FindAll(item => !excludedItems.Contains(item));
        if (itemsToChooseFrom.Count == 0)
        {
            return null;
        }
        int index = EncounterRandomStream.Range(0, itemsToChooseFrom.Count);
        AItem item = itemsToChooseFrom[index];
        return item;
    }
}

