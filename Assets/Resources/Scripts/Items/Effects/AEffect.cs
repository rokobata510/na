using System.Collections.Generic;
using UnityEngine;

public abstract class AEffect: ScriptableObject
{
    public bool standalone = true;
    public void TriggerEffect(GameObject target) => TriggerEffect(new List<GameObject>() { target });
    public abstract void TriggerEffect(List<GameObject> targets);

}