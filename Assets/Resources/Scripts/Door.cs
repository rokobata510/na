using UnityEngine;
using UnityEngine.SceneManagement;

class Door : AProp, IInteractable
{
    //TODO: ne string alapján keresd meg a target scene-t
    public string targetScene;

    public void Interact()
    {
        Debug.Log("Interacting with door");
        SceneManager.LoadScene(targetScene);
    }
}

