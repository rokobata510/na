
using UnityEngine;
using UnityEngine.SceneManagement;

public class AEndOfRunScreen : MonoBehaviour
{
    public void DestroyMap()
    {
        GameObject map = GameObject.Find("Map");
        map.GetComponent<Map>().playerOccupiedNode = null;
        Map.mapHasBeenGenerated = false;
        
        Destroy(map);
    }
    public void MainMenu()
    {
        DestroyMap();
        SceneManager.LoadScene("MainMenu");
    }
    public void NewRun()
    {
        RandomManager.WorldSeed(System.DateTime.Now.Millisecond + System.DateTime.Now.Second * 1000);
        DestroyMap();
        SceneManager.LoadScene("Map");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MainMenu();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            NewRun();
        }
    }
}

