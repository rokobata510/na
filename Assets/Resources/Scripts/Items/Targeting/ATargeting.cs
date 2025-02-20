using System.Collections.Generic;
using UnityEngine;
public abstract class ATargeting : ScriptableObject
{
    public List<GameObject> affectedGameObjects = new();
    public abstract void FindAndSetAffectedGameObjects();
    public void AddToAffectedGameObjects(GameObject gameObject)
    {
        if (!affectedGameObjects.Contains(gameObject))
        {
            affectedGameObjects.Add(gameObject);
        }
    }

    public void RemoveFromAffectedGameObjects(GameObject gameObject)
    {
        affectedGameObjects.Remove(gameObject);
    }
}
