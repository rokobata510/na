
public abstract class AEncounterDirector : ADirector
{
    public override void SetupFields(ADirectorContainer directorContainer)
    {
        SetupFields((EncounterDirectorContainer)directorContainer);
    }
    public abstract void SetupFields(EncounterDirectorContainer directorContainer);
}

