using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "AvoidMeleeRangeMovementStrategy", menuName = "MovementStrategies/AvoidMeleeRangeMovementStrategy")]

public class AvoidMeleeRangeMovementStrategy : AEnemyMovementStrategy
{

    protected override void SetMovementDirection(GameObject origin)
    {
        GameObject playerGameObject = GameObject.Find("Player");
        targetPosition = (UnnormalizedVector3)playerGameObject.transform.position;
        if (CanSeeTarget())
        {
            UpdateLastSeenTargetPosition();
        }
        if (!StandingOnTarget() && EnemyHasBeenSeen())
        {
            if (Vector2.Distance(origin.transform.position, LastSeenTargetPosition) < 3)
            {

                nextStepPosition = PathFinding.FindPathAwayFromTarget(origin, targetGameObject, 5, 50)[0];


            }
            else if (Vector2.Distance(origin.transform.position, LastSeenTargetPosition) > 6)
            {
                nextStepPosition = PathFinding.FindPathTowardsTarget(origin, targetGameObject, 5, 50)[0];
            }
            else
            {
                nextStepPosition = new NodeVector3();
            }
            Debug.DrawRay(origin.transform.position, nextStepPosition, Color.blue);
            return;
        }
        else
        {
            nextStepPosition = new NodeVector3();
        }

        //TODO: a szám ne magic number legyen, hanem egy rendes változó
        bool CanSeeTarget() => SightChecker.CanSeeTarget(origin.transform.position, targetPosition, 10000);
        void UpdateLastSeenTargetPosition() => targetGameObject.transform.position = ((UnnormalizedVector3)targetGameObject.transform.position).RoundedToHalves;
        bool StandingOnTarget()
        {
            float maxDistanceBetweenThisAndTheTarget = 0.5f;
            return Vector3.Distance(origin.transform.position, LastSeenTargetPosition) < maxDistanceBetweenThisAndTheTarget;
        }

        bool EnemyHasBeenSeen() => LastSeenTargetPosition != new UnnormalizedVector3();
    }


}

