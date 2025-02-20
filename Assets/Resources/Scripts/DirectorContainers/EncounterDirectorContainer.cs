using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterDirectorContainer : ADirectorContainer
{
    AMapNode node;
    public AMapNode Node { get => node; private set => node = value; }

    Encounter encounter;
    public Encounter Encounter { get => encounter; private set => encounter = value; }

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
        Node = GameObject.Find("Map").GetComponent<Map>().playerOccupiedNode;
        Encounter = Node.Encounters[EncounterRandomStream.Range(0, Node.Encounters.Count)];
    }
}
