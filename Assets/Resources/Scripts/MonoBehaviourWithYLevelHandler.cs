using UnityEngine;

public abstract class MonoBehaviourWithYLevelHandler : MonoBehaviour
{
    public void Update()
    {
        HandleYLevel();
        AdditionalUpdate();
    }

    protected virtual void AdditionalUpdate() { }

    void HandleYLevel()
    {
        //transform.position = new UnnormalizedVector3(transform.position.x, transform.position.y, transform.position.y);

    }
}
