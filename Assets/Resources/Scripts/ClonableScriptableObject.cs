using UnityEngine;

public class ClonableScriptableObject:ScriptableObject 
{
    public ClonableScriptableObject Clone()
    {
        return (ClonableScriptableObject)MemberwiseClone();
    }
}

