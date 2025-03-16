using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableCamera : MonoBehaviour
{
    public float DragMulitplier = 1;
    public float maxY = 10;
    public float minY = 0;
    public EventSystem eventSystem;
    public GraphicRaycaster graphicRaycaster;
    private UnnormalizedVector3 OriginWorldSpace = new();
    private bool locked;
    void Start()
    {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        graphicRaycaster = GameObject.Find("UI").GetComponent<GraphicRaycaster>();
        GameObject mapGameObject = GameObject.Find("Map");
        Map mapScript = mapGameObject.GetComponent<Map>();
        minY = mapGameObject.transform.position.y;
        maxY = mapGameObject.transform.position.y + mapScript.MaxRows + 1;
        transform.position = new Vector3(mapGameObject.transform.position.x + mapScript.MaxColumns / 2, minY, transform.position.z);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsPointerOverUIElement(out List<RaycastResult> hitUI))
            {
                foreach (RaycastResult hit in hitUI)
                {
                    if (hit.gameObject.name == "Handle")
                    {
                        locked = true;
                        return;
                    }
                }

            }
            OriginWorldSpace = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            return;
        }
        if (Input.GetMouseButtonUp(0))
        {
            locked = false;
        }
        if (locked)
            return;
        if (Input.GetMouseButton(0))
        {
            UnnormalizedVector3 MovementSinceLastFrame = (OriginWorldSpace - Camera.main.ScreenToViewportPoint(Input.mousePosition)) * DragMulitplier;
            transform.Translate(new UnnormalizedVector3(0, MovementSinceLastFrame.Y), Space.World);
            OriginWorldSpace = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            return;
        }
        if (Input.mouseScrollDelta.y != 0 && Time.timeScale > 0)
        {
            transform.Translate(new UnnormalizedVector3(0, Input.mouseScrollDelta.y), Space.World);
            if (transform.position.y > maxY)
            {
                transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
            }
            else if (transform.position.y < minY)
            {
                transform.position = new Vector3(transform.position.x, minY, transform.position.z);
            }
        }



    }
    private bool IsPointerOverUIElement(out List<RaycastResult> hitUI)
    {
        PointerEventData pointerEventData = new(eventSystem)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new();
        graphicRaycaster.Raycast(pointerEventData, results);

        if (results.Count > 0)
        {
            hitUI = results;
            return true;
        }

        hitUI = null;
        return false;
    }

}
