using System.Collections.Generic;
using UnityEngine;

public class ADirectorContainer : MonoBehaviour
{
    protected List<ADirector> directors = new();
    public virtual void Start()
    {
        directors.AddRange(GetComponentsInChildren<ADirector>());
        StartAndSetupDirectors();
    }

    protected void StartAndSetupDirectors()
    {
        foreach (ADirector director in directors)
        {
            director.SetupFields(this);
        }

        foreach (ADirector director in directors)
        {
            Debug.Log("Starting director " + director.name);
            director.StartDirector();
        }
    }

    void Update()
    {
        foreach (ADirector director in directors)
        {
            director.UpdateDirector();
        }
    }
}