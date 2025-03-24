using UnityEngine;

public class BossAI : EnemyAI
{
    public GameObject superDuperEndChest;
    public void OnEnable()
    {
        superDuperEndChest = FindFirstObjectByType<SuperDuperEndChest>(FindObjectsInactive.Include).gameObject;
        // when the boss dies, it should enable the super duper end chest
        Events.OnDeath.AddListener(() => superDuperEndChest.SetActive(true));
    }
}
