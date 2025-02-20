using System;

public class MapDirectorContainer : ADirectorContainer
{
    public override void Start()
    {
        base.Start();
        foreach (ADirector director in directors)
        {
            if (director is not AMapDirector)
            {
                throw new Exception("All directors in MapDirectorContainer must be MapDirectors");
            }
        }
    }
}

