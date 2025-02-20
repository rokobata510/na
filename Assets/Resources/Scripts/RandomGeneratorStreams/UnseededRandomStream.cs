public class UnseededRandomStream : IRandomStream
{
    public static float Range(float min, float max) => RandomManager.UnseededRange(min, max);
    public static int Range(int min, int max) => RandomManager.UnseededRange(min, max);
    float IRandomStream.Range(float min, float max) => RandomManager.UnseededRange(min, max);
    int IRandomStream.Range(int min, int max) => RandomManager.UnseededRange(min, max);
    public static float Value() => RandomManager.UnseededValue();
}