using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ListOfEveryItem", menuName = "Items/ListOfEveryItem")]
public class ListOfEveryItem : ScriptableObject
{
    private static ListOfEveryItem _instance;

    public static ListOfEveryItem Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = CreateInstance<ListOfEveryItem>();
            }
            return _instance;
        }
    }

    public List<AItem> items;
    private void OnEnable()
    {
        items = new List<AItem>(Resources.LoadAll<AItem>("ScriptableObjects/Items"));
    }
}
