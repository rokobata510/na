
using System;
using System.Collections;
using UnityEngine;
[CreateAssetMenu(fileName = "ProjectileSpawner", menuName = "Encounter/Gameplay/ProjectileSpawner")]
public class ProjectileSpawner : ScriptableObject
{
    public virtual GameObject InstantiateProjectile(ProjectileSpawningProps props)
    {
        GameObject projectileGameObject = Instantiate(props.projectile.gameObject, props.position, Quaternion.identity);
        AProjectile projectileScript = projectileGameObject.GetComponent<AProjectile>();
        projectileScript.LoadProps(props);
        projectileScript.Fire(props.direction);
        return projectileGameObject;
    }
}


