
using UnityEngine;

public class EnemyNode : AMapNode
{
    public new void Start()
    {
        base.Start();

    }
    public override void EnterEncounter()
    {
        Encounter encounter = Encounters[Random.Range(0, Encounters.Count)];

    }
    protected GameObject InstantiateEncounter(Encounter encounter)
    {
        GameObject newEncounter = Instantiate(encounter.gameObject, transform.position, Quaternion.identity);
        return newEncounter;
    }
}

