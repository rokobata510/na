using System;
using UnityEngine;
public static class RandomManager
{
    //this class holds two copies of the random state, one seeded and one not

    static UnityEngine.Random.State  _seededRandomState;
    public static UnityEngine.Random.State SeededRandomState
    {
        get
        {
            return _seededRandomState;
        }
        set
        {
            _seededRandomState = value;
        }
    }
    static UnityEngine.Random.State _unseededRandomState;
    public static UnityEngine.Random.State UnseededRandomState
    {
        get
        {
            return _unseededRandomState;
        }
        set
        {
            _unseededRandomState = value;
        }
    }
    static U WithRandomState<U>(Func<U> randomFunction, UnityEngine.Random.State state)
    {
        UnityEngine.Random.state = state;
        U result = randomFunction();
        state = UnityEngine.Random.state;
        return result;
    }
    public static float SeededRange(float min, float max)
    {
        return WithRandomState(() => UnityEngine.Random.Range(min, max), _seededRandomState);
    }
    public static int SeededRange(int min, int max)
    {
        return WithRandomState(() => UnityEngine.Random.Range(min, max), _seededRandomState);
    }
    public static float UnseededRange(float min, float max)
    {
        return WithRandomState(() => UnityEngine.Random.Range(min, max), _unseededRandomState);
    }
    
    public static int UnseededRange(int min, int max)
    {
        return WithRandomState(() => UnityEngine.Random.Range(min, max), _unseededRandomState);
    }
}

