public abstract class AHasReferenceToEncounterDirector : AEncounterDirector
{
    public Encounter encounter;

    public override void SetupFields(EncounterDirectorContainer directorContainer)
    {
        encounter = directorContainer.Encounter;
    }

}
