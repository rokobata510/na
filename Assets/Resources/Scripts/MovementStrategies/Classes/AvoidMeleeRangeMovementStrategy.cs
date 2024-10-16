using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "AvoidMeleeRangeMovementStrategy", menuName = "ScriptableObjects/MovementStrategies/AvoidMeleeRangeMovementStrategy", order = 1)]

public class AvoidMeleeRangeMovementStrategy : AEnemyMovementStrategy
{

    protected override void SetMovementDirection(UnnormalizedVector3 origin)
    {
        GameObject playerGameObject = GameObject.Find("Player");
        targetPosition = (UnnormalizedVector3)playerGameObject.transform.position;
        if (CanSeeTarget())
        {
            UpdateLastSeenTargetPosition();
        }
        if (!StandingOnTarget() && EnemyHasBeenSeen())
        {
            if (Vector2.Distance(origin, lastSeenTargetPosition) < 3)
            {

                nextStepPosition = PathFinding.FindPathAwayFromTarget(origin, lastSeenTargetPosition, 5, 50)[0];


            }
            else if (Vector2.Distance(origin, lastSeenTargetPosition) > 6)
            {
                nextStepPosition = PathFinding.FindPathTowardsTarget(origin, lastSeenTargetPosition, 5, 50)[0];
            }
            else
            {
                nextStepPosition = new NodeVector3();
            }
            Debug.DrawRay(origin, nextStepPosition, Color.blue);
            return;
        }
        else
        {
            nextStepPosition = new NodeVector3();
        }

        //TODO: a szám ne magic number legyen, hanem egy rendes változó
        bool CanSeeTarget() => SightChecker.CanSeeTarget(origin, targetPosition, 10000);
        void UpdateLastSeenTargetPosition() => lastSeenTargetPosition = targetPosition;
        bool StandingOnTarget()
        {
            float maxDistanceBetweenThisAndTheTarget = 0.5f;
            return Vector3.Distance(origin, lastSeenTargetPosition) < maxDistanceBetweenThisAndTheTarget;
        }

        bool EnemyHasBeenSeen() => lastSeenTargetPosition != new UnnormalizedVector3();
    }


}

