using UnityEngine;

public abstract class ADirector : MonoBehaviour
{
    public abstract void SetupFields(ADirectorContainer directorContainer);
    public virtual void StartDirector() { }
    public virtual void UpdateDirector() { }
}