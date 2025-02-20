using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
    

public class MapNodeEvents
{
    public UnityEvent OnHover = new();
    public UnityEvent OnUnhover = new();
    public UnityEvent OnClick = new();
    public UnityEvent OnEnter = new();
    public UnityEvent OnPlayerOccupied = new();
    public UnityEvent OnPlayerLeft = new();
}

