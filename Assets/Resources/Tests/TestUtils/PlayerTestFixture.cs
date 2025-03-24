using UnityEngine;

public class PlayerTestFixture : MonoBehaviour
{
    [SerializeField] public GameObject playerPrefab;

    public void Awake()
    {
        playerPrefab.GetComponent<PlayerScript>().deathScreen = GameObject.Find("DeathScreen");
    }
}