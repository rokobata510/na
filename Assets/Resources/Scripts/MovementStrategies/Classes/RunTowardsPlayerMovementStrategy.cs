using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
[CreateAssetMenu(fileName = "RunTowardsPlayerMovementStrategy", menuName = "ScriptableObjects/MovementStrategies/RunTowardsPlayerMovementStrategy", order = 1)]
public class RunTowardsPlayerMovementStrategy : AEnemyMovementStrategy
{

    protected override void SetMovementDirection(UnnormalizedVector3 origin)
    {

        targetPosition = targetGameObject.transform.position;
        bool canSeeTarget = CanSeeTarget();

        if (canSeeTarget)
        {
            UpdateLastSeenTargetPosition();
        }

        if (StandingOnTarget())
        {
            nextStepPosition = new UnnormalizedVector3();
        }
        else if (EnemyHasBeenSeen())
        {
            if (canSeeTarget)
            {
                SetNextStepPositionToANodeVectorTowardsTheTarget();
            }
            else
            {
                SetNextStepPositionToFirstStepOfThePath();
            }

        }
        Debug.DrawRay(origin, nextStepPosition, Color.blue, 1);

        //TODO: a 10000 ne magic number legyen, hanem egy rendes változó
        bool CanSeeTarget() => SightChecker.CanSeeTarget(origin, targetPosition, 10000);
        void UpdateLastSeenTargetPosition() => lastSeenTargetPosition = ((UnnormalizedVector3)targetGameObject.transform.position).RoundedToHalves;
        bool StandingOnTarget()
        {
            float maxDistanceBetweenThisAndTheTarget = 1;
            return Vector2.Distance(origin, lastSeenTargetPosition) < maxDistanceBetweenThisAndTheTarget;
        }

        bool EnemyHasBeenSeen() => lastSeenTargetPosition != new UnnormalizedVector3();
        void SetNextStepPositionToFirstStepOfThePath() => nextStepPosition = PathFinding.FindPathTowardsTarget(origin, lastSeenTargetPosition, 50, 20)[0] ?? new();
        //void SetNextStepTargetToFirstStepOfThePathWithReducedCost() => nextStepPosition = PathFinding.FindPathTowardsTarget(origin, lastSeenTargetPosition, 5, 20)[0] ?? new();
        
        void SetNextStepPositionToANodeVectorTowardsTheTarget()
        {

            UnnormalizedVector3 vector = lastSeenTargetPosition - origin;
            if (vector == Vector2.zero)
            {
                nextStepPosition = new NodeVector3();
                return;
            }
            Dictionary<int, NodeVector3> degreesToNodeVectors = new()
            {
                {0, new NodeVector3(0.5f,0)},
                {45, new NodeVector3(0.5f,0.5f)},
                {90, new NodeVector3(0,0.5f)},
                {135, new NodeVector3(-0.5f,0.5f)},
                {180, new NodeVector3(-0.5f,0)},
                {225, new NodeVector3(-0.5f,-0.5f)},
                {270, new NodeVector3(0,-0.5f)},
                {315, new NodeVector3(0.5f,-0.5f)}
            };
            float angle = Mathf.Atan2(vector.Y, vector.X) * Mathf.Rad2Deg;
            if (angle < 0)
            {
                angle += 360;
            }
            else if (angle >= 360)
            {
                angle -= 360;
            }
            KeyValuePair<int, NodeVector3>[] sortedNodeVectors = degreesToNodeVectors.OrderBy(pair =>
            {
                float nodeAngle = Mathf.Atan2(pair.Value.Y, pair.Value.X) * Mathf.Rad2Deg;
                float angleDifference = Mathf.DeltaAngle(angle, nodeAngle);

                return Mathf.Abs(angleDifference);
            }).ToArray();

            foreach (KeyValuePair<int, NodeVector3> sortedNodeVector in sortedNodeVectors)
            {
                float rayLength = 1f;
                Vector3 offset = sortedNodeVector.Value * 1.25f + Quaternion.Euler(0, 0, -90) * sortedNodeVector.Value * (rayLength / 2);
                Vector3 direction = Quaternion.Euler(0, 0, 90) * sortedNodeVector.Value;
                int layerMask = (1 << LayerMask.NameToLayer("Walls")) | (1 << LayerMask.NameToLayer("Enemy"));
                RaycastHit2D[] hits = Physics2D.RaycastAll(origin + offset, direction, rayLength, layerMask);
                Color rayColor = hits.Length > 0 ? Color.red : Color.green;
                Debug.DrawLine(origin + offset, origin + offset + direction * rayLength, rayColor, 2f);

                if (hits.Length == 0)
                {
                    nextStepPosition = sortedNodeVector.Value;
                    return;
                }
                else
                {
                    Debug.Log("Raycast hit wall");
                }
            }

            nextStepPosition = new NodeVector3();


        }
    }

}

