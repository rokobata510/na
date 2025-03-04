using UnityEngine;

public abstract class ATakesUpScreen: MonoBehaviour
{
    public static ATakesUpScreen activeGameObject;

    public void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Escape)
        {
            Hide();
        }
    }
    public abstract void Hide();
}

