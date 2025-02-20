public abstract class AMapDirector: ADirector
{
    public override void SetupFields(ADirectorContainer directorContainer)
    {
        SetupFields((MapDirectorContainer)directorContainer);
    }
    public abstract void SetupFields(MapDirectorContainer directorContainer);

}
