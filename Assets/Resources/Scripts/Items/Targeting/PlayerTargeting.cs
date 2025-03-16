using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerTargeting", menuName = "Items/Targeting/PlayerTargeting")]

public class PlayerTargeting : ATargeting
{
    public override void FindAndSetAffectedGameObjects()
    {
        affectedGameObjects = new List<GameObject>() { GameObject.Find("Player") };
    }
}

