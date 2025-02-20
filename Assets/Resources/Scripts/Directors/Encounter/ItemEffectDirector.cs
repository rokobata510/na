using System.Collections;
using System.Collections.Generic;

public class ItemEffectDirector : AEncounterDirector
{
    public List<AItem> items;
    public List<IEnumerator> currentlyRunningCoroutines = new();
    public override void SetupFields(EncounterDirectorContainer directorContainer)
    {
        foreach (AItem item in items)
        {
            item.targeting.FindAndSetAffectedGameObjects();
            item.RegisterTrigger(this);
        }
    }

    public override void StartDirector()
    {
    }

    public override void UpdateDirector()
    {
        foreach (AItem item in items)
        {
            if (item is OnUpdateItem)
            {
                item.Effect();
            }
        }
    }
    public void TriggerCoroutine(IEnumerator action)
    {
        StartCoroutine(TrackedCoroutine(action));
        currentlyRunningCoroutines.Add(action);
    }

    private IEnumerator TrackedCoroutine(IEnumerator action)
    {
        yield return action;
        currentlyRunningCoroutines.Remove(action);
    }
}
