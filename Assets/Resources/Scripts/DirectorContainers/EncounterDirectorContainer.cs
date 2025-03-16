using System;
using UnityEngine;

public class EncounterDirectorContainer : ADirectorContainer
{
    public AMapNode Node;

    public Encounter Encounter;

    public override void Start()
    {
        directors.AddRange(GetComponentsInChildren<ADirector>());
        foreach (ADirector director in directors)
        {
            if (director is not AEncounterDirector)
            {
                throw new Exception("All directors in EncounterDirectorContainer must be EncounterDirectors");
            }
        }
        SetNodeAndEncounter();
        StartAndSetupDirectors();

    }

    private void SetNodeAndEncounter()
    {
        GameObject mapObject = GameObject.Find("Map");
        if (mapObject == null)
        {
            Debug.Log("Map GameObject not found in the scene.");
            return;
        }
        Node = mapObject.GetComponent<Map>().playerOccupiedNode;

        Encounter = Node.Encounters[EncounterRandomStream.Range(0, Node.Encounters.Count)];
    }
}
