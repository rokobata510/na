using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "PlayerTargeting", menuName = "Items/Targeting/PlayerTargeting")]

public class PlayerTargeting : ATargeting
{
    public override void FindAndSetAffectedGameObjects()
    {
        affectedGameObjects = new List<GameObject>() { GameObject.Find("Player") };
    }
}

