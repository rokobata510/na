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
}

