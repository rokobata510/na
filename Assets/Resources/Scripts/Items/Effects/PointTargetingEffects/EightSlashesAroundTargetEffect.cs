
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EightSlashesAroundTargetEffect", menuName = "Items/Effects/EightSlashesAroundTargetEffect")]
public class EightSlashesAroundTargetEffect : APointTargetingEffect
{
    public AProjectile projectile;
    public int damage;
    public int knockback;
    public float speed;
    public float projectileLifetime;
    public NormalizedVector3 rotation = new(0, 1, 0);
    ProjectileSpawner _projectileSpawner;
    ProjectileSpawner ProjectileSpawner
    {
        get
        {
            if (_projectileSpawner == null)
            {
                _projectileSpawner = new();
            }
            return _projectileSpawner;
        }
    }

    public override void TriggerEffect(List<GameObject> targets)
    {
        ProjectileSpawningProps props;
        foreach (var target in targets)
        {
            for (int i = 0; i < 8; i++)
            {
                rotation = Quaternion.Euler(0, 0, 45 * i) * rotation;
                props = new(projectile, target.transform.position, rotation, damage, knockback, 5, projectileLifetime, target.tag, target.layer);
                ProjectileSpawner.InstantiateProjectile(props);
            }
        }
    }
}

