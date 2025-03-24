using NUnit.Framework;
using System.Collections;
using System.Linq;
using Unity.Properties;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class MapTests : InputTestFixture
{
    [UnityTest]
    public IEnumerator MapSceneCanBeLoaded()
    {
        // Setup
        RandomManager.WorldSeed(0);

        SceneManager.LoadScene("Map", LoadSceneMode.Single);
        yield return null;
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Map");
        // Test
        Assert.AreEqual("Map", SceneManager.GetActiveScene().name, "Map scene was not loaded");
        // Teardown
        Object.Destroy(GameObject.Find("Map"));
        yield return null;
    }

    [UnityTest]
    public IEnumerator MapSceneHasMap()
    {
        // Setup
        RandomManager.WorldSeed(0);
        Map.mapHasBeenGenerated = false;
        SceneManager.LoadScene("Map", LoadSceneMode.Single);
        yield return null;
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Map");
        // Test
        GameObject map = GameObject.Find("Map");
        Assert.IsNotNull(map, "Map object not found");
        // Teardown
        Object.Destroy(map);
        yield return null;
    }

    [UnityTest]
    public IEnumerator SpawnedMapHasNodes()
    {
        // Setup
        RandomManager.WorldSeed(0);
        Map.mapHasBeenGenerated = false;
        SceneManager.LoadScene("Map", LoadSceneMode.Single);
        yield return null;
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Map");
        // Test
        GameObject map = GameObject.Find("Map");
        Assert.IsNotNull(map, "Map object not found");
        Assert.Greater(map.transform.childCount, 0, "Map has no nodes");
        // Teardown
        Object.Destroy(map);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShopNodeCanBeOpened()
    {

        // Setup
        RandomManager.WorldSeed(0);
        Map.mapHasBeenGenerated = false;
        SceneManager.LoadScene("Map", LoadSceneMode.Single);
        yield return null;
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Map");
        Keyboard keyboard = InputSystem.AddDevice<Keyboard>();
        Mouse mouse = InputSystem.AddDevice<Mouse>();

        // Test
        GameObject map = GameObject.Find("Map");
        ShopNode[] shopNodes = map.GetComponentsInChildren<ShopNode>();
        ShopNode shopNodeToEnter = shopNodes.First(currentNode => currentNode.row == 1);
        map.GetComponent<Map>().EnterHitNode(shopNodeToEnter);
        Press(keyboard.spaceKey);
        yield return null;
        //assert that the CloseChopButton object is enabled
        Assert.IsTrue(GameObject.Find("CloseShopButton").GetComponent<UnityEngine.UI.Image>().enabled, "CloseShopButton object was not found in the scene");
        // Teardown
        Object.Destroy(map);
        yield return null;
    }

    [UnityTest]
    public IEnumerator EnemyNodeCanBeEntered()
    {
        // Setup
        RandomManager.WorldSeed(0);
        Map.mapHasBeenGenerated = false;
        SceneManager.LoadScene("Map", LoadSceneMode.Single);
        yield return null;
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Map");
        Keyboard keyboard = InputSystem.AddDevice<Keyboard>();
        Mouse mouse = InputSystem.AddDevice<Mouse>();
        // Test
        GameObject map = GameObject.Find("Map");
        EnemyNode[] enemyNodes = map.GetComponentsInChildren<EnemyNode>();
        EnemyNode enemyNodeToEnter = enemyNodes.First(currentNode => currentNode.row == 1 && !currentNode.gameObject.name.Contains("Elite"));
        map.GetComponent<Map>().EnterHitNode(enemyNodeToEnter);
        Press(keyboard.spaceKey);
        yield return null;
        //wait for the scene to be Encounter
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Encounter");
        //assert that enemies are spawned
        Assert.Greater(GameObject.FindGameObjectsWithTag("Enemy").Length, 0, "No enemies were spawned");
        // Teardown
        Object.Destroy(map);
        yield return null;
    }
    [UnityTest]
    public IEnumerator EliteNodeCanBeEntered()
    {
        // Setup
        RandomManager.WorldSeed(0);
        Map.mapHasBeenGenerated = false;
        SceneManager.LoadScene("Map", LoadSceneMode.Single);
        yield return null;
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Map");
        Keyboard keyboard = InputSystem.AddDevice<Keyboard>();
        Mouse mouse = InputSystem.AddDevice<Mouse>();
        // Test
        GameObject map = GameObject.Find("Map");
        EnemyNode[] eliteNodes = map.GetComponentsInChildren<EnemyNode>();
        EnemyNode eliteNodeToEnter = eliteNodes.First(currentNode => currentNode.row == 1 && currentNode.gameObject.name.Contains("Elite"));
        map.GetComponent<Map>().EnterHitNode(eliteNodeToEnter);
        Press(keyboard.spaceKey);
        yield return null;
        //wait for the scene to be Encounter
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Encounter");
        //assert that enemies are spawned
        Assert.Greater(GameObject.FindGameObjectsWithTag("Enemy").Length, 0, "No enemies were spawned");
        // Teardown
        Object.Destroy(map);
        yield return null;
    }

    [UnityTest]
    //we check if there is an encounter gameobject in the scene, if there is a player gameobject in the scene and if the player has a player script attached to it
    //we also check that there is an object tagged as "EndChest" in the scene
    //we also check that there is an object tagged as "Enemy" in the scene
    public IEnumerator EnemyNodeInitializedCorrectly()
    {
        // Setup
        RandomManager.WorldSeed(0);
        Map.mapHasBeenGenerated = false;
        SceneManager.LoadScene("Map", LoadSceneMode.Single);
        yield return null;
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Map");
        Keyboard keyboard = InputSystem.AddDevice<Keyboard>();
        Mouse mouse = InputSystem.AddDevice<Mouse>();
        // Test
        GameObject map = GameObject.Find("Map");
        EnemyNode[] enemyNodes = map.GetComponentsInChildren<EnemyNode>();
        EnemyNode enemyNodeToEnter = enemyNodes.First(currentNode => currentNode.row == 1 && !currentNode.gameObject.name.Contains("Elite"));
        map.GetComponent<Map>().EnterHitNode(enemyNodeToEnter);
        Press(keyboard.spaceKey);

        yield return null;
        //wait for the scene to be Encounter
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Encounter");
        //assert that the player object is in the scene
        Assert.IsNotNull(GameObject.Find("Player"), "Player object not found in the scene");
        //assert that the player object has a player script attached to it
        Assert.IsNotNull(GameObject.Find("Player").GetComponent<PlayerScript>(), "Player object does not have a player script attached to it");
        //assert that there is an an object with the tag "Encounter" in the scene
        Assert.IsNotNull(GameObject.FindWithTag("Encounter"), "Encounter object not found in the scene");
        //assert that the object tagged as "EndChest" is in the scene
        Assert.IsNotNull(GameObject.FindGameObjectsWithTag("EndChest"), "EndChest object not found in the scene");
        //assert that the object tagged as "Enemy" is in the scene
        Assert.IsNotNull(GameObject.FindGameObjectsWithTag("Enemy"), "Enemy object not found in the scene");
        // Teardown
        Object.Destroy(map);
        yield return null;
    }
}
