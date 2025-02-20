using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndChest : MonoBehaviour, IInteractable
{
    public float goldAmount;
    public float multiplierPerSecond = 0.99f;
    public int seed;
    protected GameObject rewardsRenderer;
    private EndChestRewardRenderer rewardPanel;

    public void Start()
    {
        rewardsRenderer = GameObject.Find("End Chest Reward");
        rewardPanel = rewardsRenderer.GetComponent<EndChestRewardRenderer>();
        StartCoroutine(ReduceGoldByOneEverySecondUntilEmpty());
    }

    private IEnumerator ReduceGoldByOneEverySecondUntilEmpty()
    {
        while (goldAmount > 0)
        {
            Debug.Log("Gold amount: " + goldAmount);
            yield return new WaitForSeconds(1);
            goldAmount*= multiplierPerSecond;
        }
    }

    public void Interact()
    {
        Debug.Log("Interacting with EndChest");
        EncounterRandomStream.Seed(seed);
        rewardPanel.SetChestData(goldAmount);
        rewardPanel.ToggleVisibility(true);
    }
}