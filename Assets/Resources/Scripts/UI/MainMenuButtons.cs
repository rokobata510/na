using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void StartGame()
    {
        string inputText = GameObject.Find("SeedInput").GetComponent<TMP_InputField>().text;

        if (!int.TryParse(inputText, out int seed))
        {
            seed = inputText.GetHashCode();
        }

        if (seed == 0)
        {
            seed = System.DateTime.Now.Millisecond + System.DateTime.Now.Second * 1000;
        }

        RandomManager.WorldSeed(seed);
        SceneManager.LoadScene("Map");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
