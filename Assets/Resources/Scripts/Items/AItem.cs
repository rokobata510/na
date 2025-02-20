using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite sprite;
    public AEffect effect;
    public ATargeting targeting;
    public ATrigger trigger;
    public List<GameObject> AffectedGameObjects => targeting.affectedGameObjects;

    public virtual void Effect()
    {
        if(!effect.standalone)
        {
            throw new Exception("Effect of " + GetType() + " must be standalone");
        }
        if (AffectedGameObjects != null)
        {
            effect.TriggerEffect(AffectedGameObjects);
        }
        else
        {
            throw new Exception("Affected GameObjects of " + GetType() + " is null");
        }
    }
    protected void FindAndSetAffectedGameObjects()
    {
        targeting.FindAndSetAffectedGameObjects();
    }
    public void RegisterTrigger(ItemEffectDirector director)
    {
        foreach (GameObject affectedGameObject in AffectedGameObjects)
        {
            trigger.RegisterTrigger((subject) => effect.TriggerEffect(affectedGameObject), director, affectedGameObject);
        }
    }
    public virtual void AddToInventory()
    {
        Inventory.Instance.Items.Add(this);
    }
}
