using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AlwaysPathFindToPlayerMovementStrategy", menuName = "MovementStrategies/AlwaysPathFindToPlayerMovementStrategy")]

public class AlwaysPathFindToPlayerMovementStrategy : RunTowardsPlayerMovementStrategy
{
    protected override void SetMovementDirection(GameObject origin)
    {
        GameObject playerGameObject = GameObject.Find("Player");
        if (targetGameObject == null)
        {
            targetGameObject = new GameObject("LastSeenPosition");
            targetGameObject.layer = playerGameObject.layer;
            targetGameObject.transform.parent = origin.transform;
        }
        targetGameObject.transform.position = playerGameObject.transform.position;

        if (Mathf.Round(targetPosition.X) != Mathf.Round(origin.transform.position.x) || Mathf.Round(targetPosition.Y) != Mathf.Round(origin.transform.position.y))
        {

            nextStepPosition = PathFinding.FindPathTowardsTarget(origin, targetGameObject)[0];
            Debug.DrawRay(origin.transform.position, nextStepPosition, Color.blue);
            return;
        }


    }
}

