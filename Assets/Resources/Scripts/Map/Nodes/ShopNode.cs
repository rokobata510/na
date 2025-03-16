
using UnityEngine;

public class ShopNode : AMapNode
{
    public override void EnterEncounter()
    {
        EncounterRandomStream.Seed(seed);
        ShopRenderer shopRenderer = GameObject.Find("ShopRenderer").GetComponent<ShopRenderer>();
        shopRenderer.ToggleVisibility();

    }
}

