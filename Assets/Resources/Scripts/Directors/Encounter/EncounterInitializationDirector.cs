using UnityEngine;
public class EncounterInitializationDirector : AHasReferenceToEncounterDirector
{

    public EnemyNode node;

    public override void SetupFields(EncounterDirectorContainer directorContainer)
    {
        base.SetupFields(directorContainer);
        node = (EnemyNode)directorContainer.Node;
        ItemEffectDirector itemEffectDirector = GameObject.Find("ItemEffectDirector").GetComponent<ItemEffectDirector>();
        itemEffectDirector.items = Inventory.Instance.EquippedItems;
    }

    public override void StartDirector()
    {
        EncounterRandomStream.Seed(node.seed);
        encounter = node.Encounters[EncounterRandomStream.Range(0, node.Encounters.Count)];
        PlayerScript player = GameObject.Find("Player").GetComponent<PlayerScript>();
        Destroy(player.weaponScript.gameObject);
        player.weapon = Inventory.Instance.EquippedWeapon.gameObject;
        ((PlayerEvents)player.Events).OnWeaponChange.Invoke();
        GameObject encounterGameObject = Instantiate(encounter.gameObject, new UnnormalizedVector3(0, 0), Quaternion.identity);
        foreach (Transform child in encounterGameObject.transform)
        {
            if (child.CompareTag("EndChest"))
            {
                child.GetComponent<EndChest>().seed = node.seed;
            }
        }

    }
}

