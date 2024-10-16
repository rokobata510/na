public class SeededRandomStream: IRandomStream
{
    public static float Range(float min, float max)
    {
        return RandomManager.SeededRange(min, max);
    }
    public static int Range(int min, int max)
    {
        return RandomManager.SeededRange(min, max);
    }
    float IRandomStream.Range(float min, float max)
    {
        return RandomManager.SeededRange(min, max);
    }
    int IRandomStream.Range(int min, int max)
    {
        return RandomManager.SeededRange(min, max);
    }
}
