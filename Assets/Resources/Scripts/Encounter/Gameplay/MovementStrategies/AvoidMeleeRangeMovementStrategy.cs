using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "AvoidMeleeRangeMovementStrategy", menuName = "MovementStrategies/AvoidMeleeRangeMovementStrategy")]

public class AvoidMeleeRangeMovementStrategy : AEnemyMovementStrategy
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
        bool canSeeTarget = CanSeeTarget();


        if (canSeeTarget)
        {
            UpdateTargetGameObjectPosition();
        }

        if (!StandingOnTarget())
        {
            if (EnemyHasBeenSeen())
            {
                if (canSeeTarget)
                {
                    SetNextStepPositionToANodeVectorAwayFromTheTarget();
                }
                else
                {
                    SetNextStepPositionToFirstStepOfThePath();
                }

            }
        }
        else
        {
            nextStepPosition = new UnnormalizedVector3();
        }

        //TODO: a szám ne magic number legyen, hanem egy rendes változó
        bool CanSeeTarget() => SightChecker.CanSeeTarget(origin.transform.position, targetPosition, 10000);
        void UpdateTargetGameObjectPosition()
        {
            targetGameObject.transform.position = ((UnnormalizedVector3)targetGameObject.transform.position).RoundedToHalves;
        }
        void SetNextStepPositionToFirstStepOfThePath()
        {
            nextStepPosition = PathFinding.FindPathTowardsTarget(origin, targetGameObject, 50, 20)[0] ?? new();
        }
        bool StandingOnTarget()
        {
            float maxDistanceBetweenThisAndTheTarget = 0.5f;
            return Vector3.Distance(origin.transform.position, LastSeenTargetPosition) < maxDistanceBetweenThisAndTheTarget;
        }

        bool EnemyHasBeenSeen() => LastSeenTargetPosition != new UnnormalizedVector3();
        void SetNextStepPositionToANodeVectorTowardsTheTarget()
        {

            UnnormalizedVector3 vector = LastSeenTargetPosition - origin.transform.position;
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
                RaycastHit2D[] hits = Physics2D.RaycastAll(origin.transform.position + offset, direction, rayLength);

                List<RaycastHit2D> validHits = new List<RaycastHit2D>();
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider.CompareTag("Walls") || hit.collider.CompareTag("Enemy"))
                    {
                        validHits.Add(hit);
                    }
                }

                Color rayColor = validHits.Count > 0 ? Color.red : Color.green;
                Debug.DrawLine(origin.transform.position + offset, origin.transform.position + offset + direction * rayLength, rayColor, 2f);

                if (validHits.Count == 0)
                {
                    nextStepPosition = sortedNodeVector.Value;
                    return;
                }
                else
                {
                    Debug.Log("Raycast hit wall or enemy");
                }
            }

            nextStepPosition = new NodeVector3();


        }
        void SetNextStepPositionToANodeVectorAwayFromTheTarget()
        {
            UnnormalizedVector3 vector = origin.transform.position - LastSeenTargetPosition;
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
                RaycastHit2D[] hits = Physics2D.RaycastAll(origin.transform.position + offset, direction, rayLength);

                List<RaycastHit2D> validHits = new();
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider.CompareTag("Walls") || hit.collider.CompareTag("Enemy"))
                    {
                        validHits.Add(hit);
                    }
                }

                Color rayColor = validHits.Count > 0 ? Color.red : Color.green;
                Debug.DrawLine(origin.transform.position + offset, origin.transform.position + offset + direction * rayLength, rayColor, 2f);

                if (validHits.Count == 0)
                {
                    nextStepPosition = sortedNodeVector.Value;
                    return;
                }
                else
                {
                    Debug.Log("Raycast hit wall or enemy");
                }
            }

            nextStepPosition = new NodeVector3();
        }
    }
    /*
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

    */

}

