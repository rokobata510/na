
using Codice.CM.Common;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DeathScreen : MonoBehaviour
{

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
    public void NewRun()
    {
        RandomManager.WorldSeed(System.DateTime.Now.Millisecond + System.DateTime.Now.Second * 1000);
        DestroyMap();
        SceneManager.LoadScene("Map");
    }

    public void MainMenu()
    {
        DestroyMap();
        SceneManager.LoadScene("MainMenu");
    }
    public void DestroyMap()
    {
        GameObject map = GameObject.Find("Map");
        Destroy(map);
    }
}

