using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[RequireComponent(typeof(Collider2D))]
public abstract class AMapNode : MonoBehaviour
{
    public int row;
    public int column;
    public List<Encounter> Encounters;
    List<AMapNode> routesFromHere = new();
    List<GameObject> linesFromHere = new();
    public MapNodeEvents events = new();
    private SpriteRenderer spriteRenderer;
    private Color defaultColor;
    private Color colorBeforeHover;
    private bool hoverHasTriggered = false;
    public int seed;
    public List<AMapNode> RoutesFromHere => routesFromHere;
    public List<GameObject> LinesFromHere => linesFromHere;
    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
        events.OnHover.AddListener(OnHover);
        events.OnUnhover.AddListener(OnUnhover);
        events.OnClick.AddListener(OnClick);
        events.OnEnter.AddListener(EnterEncounter);
        events.OnPlayerOccupied.AddListener(OnPlayerOccupied);
        events.OnPlayerLeft.AddListener(OnPlayerLeft);
    }
    public abstract void EnterEncounter();
    
    public void OnHover()
    {
        if (!hoverHasTriggered)
        {
            colorBeforeHover = spriteRenderer.color;
            transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            spriteRenderer.color = Color.gray;
            hoverHasTriggered = true;
        }
    }
    public void OnUnhover()
    {
        hoverHasTriggered = false;
        transform.localScale = new Vector3(1f, 1f, 1f);
        spriteRenderer.color = colorBeforeHover;
    }
    public void OnClick()
    {
        Debug.Log("Clicked on MapNode: " + column + "; " + row);
    }
    public void OnPlayerOccupied()
    {
        Debug.Log("Player occupied node: " + column + "; " + row);
        transform.localScale = new Vector3(2f, 2f, 2f);
        spriteRenderer.color = Color.green;
    }
    public void OnPlayerLeft()
    {
        Debug.Log("Player left node: " + column + "; " + row);
        transform.localScale = new Vector3(1f, 1f, 1f);
        spriteRenderer.color = defaultColor;
    }
}
