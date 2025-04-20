using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            targetGameObject.tag = playerGameObject.tag;
        }
        bool canSeeTarget = CanSeeTarget(playerGameObject.transform.position);

        if (canSeeTarget)
        {
            UpdateTargetGameObjectPosition();
        }

        if (!StandingOnTarget() && EnemyHasBeenSeen())
        {
            if (canSeeTarget)
            {
                if (Vector3.Distance(playerGameObject.transform.position, origin.transform.position) > 10)
                    SetNextStepPositionToANodeVectorTowardsTheTarget();
                else if (Vector3.Distance(playerGameObject.transform.position, origin.transform.position) < 5)
                    SetNextStepPositionToAnodeVectorAwayFromTheTarget();
                else
                    nextStepPosition = new NodeVector3();
            }
            else
            {
                SetNextStepPositionToFirstStepOfThePath();
            }

        }
        else
        {
            nextStepPosition = new UnnormalizedVector3();
        }
        Debug.DrawRay(origin.transform.position, nextStepPosition, Color.blue, 1);

        //TODO: a 10000 ne magic number legyen, hanem egy rendes változó
        bool CanSeeTarget(UnnormalizedVector3 target) => SightChecker.CanSeeTarget(origin.transform.position, target, 10000);
        void UpdateTargetGameObjectPosition()
        {
            targetGameObject.transform.position = ((UnnormalizedVector3)GameObject.Find("Player").transform.position).RoundedToHalves;
        }

        bool StandingOnTarget()
        {
            float maxDistanceBetweenThisAndTheTarget = 1;
            return Vector2.Distance(origin.transform.position, targetGameObject.transform.position) < maxDistanceBetweenThisAndTheTarget;
        }

        bool EnemyHasBeenSeen() => (LastSeenTargetPosition != new UnnormalizedVector3());
        void SetNextStepPositionToFirstStepOfThePath()
        {
            nextStepPosition = PathFinding.FindPathTowardsTarget(origin, targetGameObject, 20, 50)[0] ?? new();
        }
        //void SetNextStepTargetToFirstStepOfThePathWithReducedCost() => nextStepPosition = PathFinding.FindPathTowardsTarget(origin, lastSeenTargetPosition, 5, 20)[0] ?? new();

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
        void SetNextStepPositionToAnodeVectorAwayFromTheTarget()
        {
            UnnormalizedVector3 vector = origin.transform.position - LastSeenTargetPosition;
            if (vector == Vector2.zero)
            {
                nextStepPosition = new NodeVector3();
                return;
            }
            Dictionary<int, NodeVector3> degreesToNodeVectors = new()
            {
                {0, new NodeVector3(-0.5f,0)},
                {45, new NodeVector3(-0.5f,-0.5f)},
                {90, new NodeVector3(0,-0.5f)},
                {135, new NodeVector3(0.5f,-0.5f)},
                {180, new NodeVector3(0.5f,0)},
                {225, new NodeVector3(0.5f,0.5f)},
                {270, new NodeVector3(0,-0.5f)},
                {315, new NodeVector3(-0.5f,0.5f)}
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
            }
            nextStepPosition = new NodeVector3();

        }
    }
}

