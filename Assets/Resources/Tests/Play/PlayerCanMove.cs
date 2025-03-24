using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;

public class PlayerCanMove : InputTestFixture
{
    [UnityTest]
    public IEnumerator PlayerCanMoveUp()
    {
        // Setup
        Inventory.Instance = Resources.Load<Inventory>("ScriptableObjects/Items/Inventory");
        Inventory.Reset();
        SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
        yield return null;
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "TestScene");

        Keyboard keyboard = InputSystem.AddDevice<Keyboard>();
        Mouse mouse = InputSystem.AddDevice<Mouse>();
        GameObject Tom = new("Tom");
        Tom.AddComponent<Tom>();
        Tom.transform.position = new Vector3(0, 0, 0);
        mouse.WarpCursorPosition(new Vector2(100, 100));

        GameObject player = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Tests/TestUtils/TestPlayer"), new Vector3(0, 0), Quaternion.identity);
        Vector3 initialPosition = player.transform.position;
        yield return null;

        // Test
        Press(keyboard.wKey);
        yield return new WaitForSeconds(0.1f);
        Assert.AreNotEqual(initialPosition, player.transform.position, "Player did not move upwards");
        Release(keyboard.wKey);

        // Teardown
        UnityEngine.Object.Destroy(Tom);
        UnityEngine.Object.Destroy(player);
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerCanMoveLeft()
    {
        // Setup
        Inventory.Instance = Resources.Load<Inventory>("ScriptableObjects/Items/Inventory");
        Inventory.Reset();
        SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
        yield return null;
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "TestScene");

        Keyboard keyboard = InputSystem.AddDevice<Keyboard>();
        Mouse mouse = InputSystem.AddDevice<Mouse>();
        GameObject Tom = new("Tom");
        Tom.AddComponent<Tom>();
        Tom.transform.position = new Vector3(0, 0, 0);
        mouse.WarpCursorPosition(new Vector2(100, 100));

        GameObject player = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Tests/TestUtils/TestPlayer"), new Vector3(0, 0), Quaternion.identity);
        Vector3 initialPosition = player.transform.position;
        yield return null;

        // Test
        Press(keyboard.aKey);
        yield return new WaitForSeconds(0.1f);
        Assert.AreNotEqual(initialPosition, player.transform.position, "Player did not move left");
        Release(keyboard.aKey);

        // Teardown
        UnityEngine.Object.Destroy(Tom);
        UnityEngine.Object.Destroy(player);
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerCanMoveDown()
    {
        // Setup
        Inventory.Instance = Resources.Load<Inventory>("ScriptableObjects/Items/Inventory");
        Inventory.Reset();
        SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
        yield return null;
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "TestScene");

        Keyboard keyboard = InputSystem.AddDevice<Keyboard>();
        Mouse mouse = InputSystem.AddDevice<Mouse>();
        GameObject Tom = new("Tom");
        Tom.AddComponent<Tom>();
        Tom.transform.position = new Vector3(0, 0, 0);
        mouse.WarpCursorPosition(new Vector2(100, 100));

        GameObject player = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Tests/TestUtils/TestPlayer"), new Vector3(0, 0), Quaternion.identity);
        Vector3 initialPosition = player.transform.position;
        yield return null;

        // Test
        Press(keyboard.sKey);
        yield return new WaitForSeconds(0.1f);
        Assert.AreNotEqual(initialPosition, player.transform.position, "Player did not move downwards");
        Release(keyboard.sKey);

        // Teardown
        UnityEngine.Object.Destroy(Tom);
        UnityEngine.Object.Destroy(player);
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerCanMoveRight()
    {
        // Setup
        Inventory.Instance = Resources.Load<Inventory>("ScriptableObjects/Items/Inventory");
        Inventory.Reset();
        SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
        yield return null;
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "TestScene");

        Keyboard keyboard = InputSystem.AddDevice<Keyboard>();
        Mouse mouse = InputSystem.AddDevice<Mouse>();
        GameObject Tom = new("Tom");
        Tom.AddComponent<Tom>();
        Tom.transform.position = new Vector3(0, 0, 0);
        mouse.WarpCursorPosition(new Vector2(100, 100));

        GameObject player = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Tests/TestUtils/TestPlayer"), new Vector3(0, 0), Quaternion.identity);
        Vector3 initialPosition = player.transform.position;
        yield return null;

        // Test
        Press(keyboard.dKey);
        yield return new WaitForSeconds(0.1f);
        Assert.AreNotEqual(initialPosition, player.transform.position, "Player did not move right");
        Release(keyboard.dKey);

        // Teardown
        UnityEngine.Object.Destroy(Tom);
        UnityEngine.Object.Destroy(player);
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerCanAttack()
    {
        // Setup
        Inventory.Instance = Resources.Load<Inventory>("ScriptableObjects/Items/Inventory");
        Inventory.Reset();
        SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
        yield return null;
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "TestScene");

        Keyboard keyboard = InputSystem.AddDevice<Keyboard>();
        Mouse mouse = InputSystem.AddDevice<Mouse>();
        GameObject Tom = new("Tom");
        Tom.AddComponent<Tom>();
        Tom.transform.position = new Vector3(0, 0, 0);
        mouse.WarpCursorPosition(new Vector2(100, 100));

        GameObject player = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Tests/TestUtils/TestPlayer"), new Vector3(0, 0), Quaternion.identity);
        yield return null;

        Press(mouse.leftButton);
        yield return new WaitForSeconds(0.1f);

        yield return new WaitForSeconds(0.5f);

        Assert.IsNotNull(GameObject.Find("Slash(Clone)"), "Slash object was not found in the scene");


        Release(mouse.leftButton);
        UnityEngine.Object.Destroy(Tom);
        UnityEngine.Object.Destroy(player);

        yield return null;
    }

    [UnityTest]
    public IEnumerator PauseMenuOpens()
    {
        // Setup
        Inventory.Instance = Resources.Load<Inventory>("ScriptableObjects/Items/Inventory");
        Inventory.Reset();
        SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
        yield return null;
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "TestScene");
        Keyboard keyboard = InputSystem.AddDevice<Keyboard>();
        Mouse mouse = InputSystem.AddDevice<Mouse>();
        GameObject Tom = new("Tom");
        Tom.AddComponent<Tom>();
        Tom.transform.position = new Vector3(0, 0, 0);
        mouse.WarpCursorPosition(new Vector2(100, 100));
        GameObject player = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Tests/TestUtils/TestPlayer"), new Vector3(0, 0), Quaternion.identity);
        yield return null;
        yield return new WaitForSeconds(0.1f);
        Assert.IsNotNull(GameObject.Find("PauseMenu"), "PauseMenu object was not found in the scene");
        //log if the game is paused
        Debug.Log("Game is paused: " + PauseMenuManager.isPaused);
        GameObject.Find("PauseMenu").GetComponent<PauseMenuManager>().PauseGame();

        Assert.IsTrue(GameObject.Find("PauseMenu").GetComponent<UnityEngine.UI.Image>().enabled, "PauseMenu's image is not enabled");
        yield return null;
        GameObject.Find("PauseMenu").GetComponent<PauseMenuManager>().ResumeGame();
        Assert.IsFalse(GameObject.Find("PauseMenu").GetComponent<UnityEngine.UI.Image>().enabled, "PauseMenu's image is enabled, after the menu was closed");

        UnityEngine.Object.Destroy(Tom);
        UnityEngine.Object.Destroy(player);
        yield return null;
    }
}