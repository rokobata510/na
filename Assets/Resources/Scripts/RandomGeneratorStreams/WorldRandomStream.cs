public class WorldRandomStream: IRandomStream
{
    public static float Range(float min, float max) => RandomManager.WorldRange(min, max);
    public static int Range(int min, int max) => RandomManager.WorldRange(min, max);
    float IRandomStream.Range(float min, float max) => RandomManager.WorldRange(min, max);
    int IRandomStream.Range(int min, int max) => RandomManager.WorldRange(min, max);
    public static float Value() => RandomManager.WorldValue();
}
