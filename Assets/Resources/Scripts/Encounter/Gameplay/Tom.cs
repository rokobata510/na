using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tom : MonoBehaviour
{
    void Start()
    {

    }
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        transform.position = mousePosition;
    }
}
