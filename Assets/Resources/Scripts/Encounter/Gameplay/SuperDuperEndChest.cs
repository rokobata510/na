using UnityEngine;

public class SuperDuperEndChest: MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameObject winScreen = FindFirstObjectByType<WinScreen>(FindObjectsInactive.Include).gameObject;
        winScreen.SetActive(true);

    }
}

