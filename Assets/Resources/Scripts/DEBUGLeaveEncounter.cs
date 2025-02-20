using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DEBUGLeaveEncounter : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            GameObject map = GameObject.Find("Map");

            foreach (Transform child in map.transform)
            {
                if (child.TryGetComponent(out Renderer renderer))
                {
                    renderer.enabled = true;
                }
                if (child.TryGetComponent(out Collider2D collider))
                {
                    collider.enabled = true;
                }
            }

            SceneManager.LoadScene("Map");
        }
    }
}
