using UnityEngine.Events;
public class PlayerEvents: ActorEvents
{
    public UnityEvent OnRolling = new();
    public UnityEvent OnNotRolling = new();
}