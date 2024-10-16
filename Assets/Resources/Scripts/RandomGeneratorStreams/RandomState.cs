using UnityEngine;

public class RandomState
{
    public Random.State state;

    protected void InitializeRandomState(int seed)
    {
        Random.InitState(seed);
        state = Random.state;
    }
    public static implicit operator Random.State(RandomState state)
    {
        return state.state;
    }
    public static implicit operator RandomState(Random.State state)
    {
        RandomState newState = new RandomState();
        newState.state = state;
        return newState;
    }

}