
public class EncounterRandomStream : IRandomStream
{

    public static float Range(float min, float max) => RandomManager.EncounterRange(min, max);
    public static int Range(int min, int max) => RandomManager.EncounterRange(min, max);
    float IRandomStream.Range(float min, float max) => RandomManager.EncounterRange(min, max);
    int IRandomStream.Range(int min, int max) => RandomManager.EncounterRange(min, max);
    public static void Seed(int seed) => RandomManager.EncounterSeed(seed);


}

