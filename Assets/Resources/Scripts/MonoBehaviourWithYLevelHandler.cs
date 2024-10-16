using UnityEngine;

public abstract class MonoBehaviourWithYLevelHandler : MonoBehaviour
{
    // Template method defining the overall update structure
    public void Update()
    {
        HandleYLevel();
        AdditionalUpdate();
    }

    // Abstract method for derived classes to implement their specific update logic
    protected virtual void AdditionalUpdate() { }

    void HandleYLevel()
    {
        //transform.position = new UnnormalizedVector3(transform.position.x, transform.position.y, transform.position.y);

    }
}
