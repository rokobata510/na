using UnityEngine;
using UnityEngine.Events;

public class EventManager: MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public PlayerEvents PlayerEvents = new();
    public EnemyEvents EnemyEvents = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

