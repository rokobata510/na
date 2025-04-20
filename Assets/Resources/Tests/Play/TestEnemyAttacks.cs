using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class TestGobboAttacks
{
    [UnityTest]
    public IEnumerator GobboIsSpawned()
    {
        SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
        yield return null;
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "TestScene");
        GameObject gobbo = Object.Instantiate(Resources.Load<GameObject>("Tests/TestUtils/TestGobbo"), new Vector3(-2, 0), Quaternion.identity);
        Assert.IsNotNull(gobbo);
        Object.Destroy(gobbo);
    }

    [UnityTest]
    public IEnumerator SpawnedGobboSpawnsProjectile()
    {
        SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
        yield return null;
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "TestScene");

        GameObject gobbo = Object.Instantiate(Resources.Load<GameObject>("Tests/TestUtils/TestGobbo"), new Vector3(-2, 0), Quaternion.identity);

        yield return new WaitForSeconds(1f);

        Assert.IsNotNull(GameObject.Find("Slash(Clone)"), "Slash object was not found in the scene after 1 second");
        Object.Destroy(gobbo);
        if (GameObject.Find("Projectile(Clone)"))
        {
            Object.Destroy(GameObject.Find("Projectile(Clone)"));
        }

    }

    [UnityTest]
    public IEnumerator PlayerCanBeAttackedByTheGoblinSpawnedBelowIt()
    {
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

        GameObject gobbo = Object.Instantiate(Resources.Load<GameObject>("Tests/TestUtils/TestGobbo"), new Vector3(0, 0), Quaternion.identity);
        GameObject player = Object.Instantiate(Resources.Load<GameObject>("Tests/TestUtils/TestPlayer"), new Vector3(0, 2), Quaternion.identity);
        yield return new WaitForSeconds(1f);

        Assert.That(player.GetComponent<AActor>().health, Is.LessThan(100));
        Object.Destroy(gobbo);
        Object.Destroy(Tom);
        Object.Destroy(player);
        Inventory.Reset();
    }
}