public class UnseededRandomStream : IRandomStream
{
    public static float Range(float min, float max)
    {
        return RandomManager.UnseededRange(min, max);
    }
    public static int Range(int min, int max)
    {
        return RandomManager.UnseededRange(min, max);
    }
    float IRandomStream.Range(float min, float max)
    {
        return RandomManager.UnseededRange(min, max);
    }
    int IRandomStream.Range(int min, int max)
    {
        return RandomManager.UnseededRange(min, max);
    }
}