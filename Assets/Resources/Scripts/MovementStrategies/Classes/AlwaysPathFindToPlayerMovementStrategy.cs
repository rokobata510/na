using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AlwaysPathFindToPlayerMovementStrategy", menuName = "ScriptableObjects/MovementStrategies/AlwaysPathFindToPlayerMovementStrategy", order = 1)]

public class AlwaysPathFindToPlayerMovementStrategy : RunTowardsPlayerMovementStrategy
{
    protected override void SetMovementDirection(UnnormalizedVector3 origin)
    {
        GameObject playerGameObject = GameObject.Find("Player");
        UnnormalizedVector3 playerPosition = (UnnormalizedVector3)playerGameObject.transform.position;        
        if (Mathf.Round(playerPosition.X) != Mathf.Round(origin.X) || Mathf.Round(playerPosition.Y) != Mathf.Round(origin.Y))
        {

            nextStepPosition = PathFinding.FindPathTowardsTarget(origin, playerPosition)[0];
            Debug.DrawRay(origin, nextStepPosition, Color.blue);
            return;
        }


    }
}

