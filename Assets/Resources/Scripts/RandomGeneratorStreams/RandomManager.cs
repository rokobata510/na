using System;
using UnityEngine;

public static class RandomManager
{
    private static UnityEngine.Random.State worldRandomState;
    private static UnityEngine.Random.State encounterRandomState;
    private static UnityEngine.Random.State unseededRandomState;

    private static U WithRandomState<U>(Func<U> randomFunction, ref UnityEngine.Random.State state)
    {
        if (worldRandomState.Equals(default(UnityEngine.Random.State)))
        {
            worldRandomState = UnityEngine.Random.state;
        }
        if (unseededRandomState.Equals(default(UnityEngine.Random.State)))
        {
            unseededRandomState = UnityEngine.Random.state;
        }
        if (encounterRandomState.Equals(default(UnityEngine.Random.State)))
        {
            UnityEngine.Random.InitState(101);
            encounterRandomState = UnityEngine.Random.state;
        }
        UnityEngine.Random.state = state;
        U result = randomFunction();
        state = UnityEngine.Random.state;
        return result;
    }

    public static float WorldRange(float min, float max) => WithRandomState(() => UnityEngine.Random.Range(min, max), ref worldRandomState);
    public static int WorldRange(int min, int max) => WithRandomState(() => UnityEngine.Random.Range(min, max), ref worldRandomState);
    public static float WorldValue() => WithRandomState(() => UnityEngine.Random.value, ref worldRandomState);
    public static void WorldSeed(int seed) => UnityEngine.Random.InitState(seed);

    public static float EncounterRange(float min, float max) => WithRandomState(() => UnityEngine.Random.Range(min, max), ref encounterRandomState);
    public static int EncounterRange(int min, int max) => WithRandomState(() => UnityEngine.Random.Range(min, max), ref encounterRandomState);
    public static float EncounterValue() => WithRandomState(() => UnityEngine.Random.value, ref encounterRandomState);
    public static void EncounterSeed(int seed) => UnityEngine.Random.InitState(seed);

    public static float UnseededRange(float min, float max) => WithRandomState(() => UnityEngine.Random.Range(min, max), ref unseededRandomState);
    public static int UnseededRange(int min, int max) => WithRandomState(() => UnityEngine.Random.Range(min, max), ref unseededRandomState);
    public static float UnseededValue() => WithRandomState(() => UnityEngine.Random.value, ref unseededRandomState);

}
