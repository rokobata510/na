using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyNode : AMapNode
{
    public new void Start()
    {
        base.Start();

    }
    public override void EnterEncounter()
    {
        SceneManager.LoadScene("Encounter");
        var mapGameobject = GameObject.Find("Map");
        var mapTransform = mapGameobject.transform;
        foreach (Transform child in mapTransform)
        {
            if (child.TryGetComponent(out Renderer renderer))
            {
                renderer.enabled = false;
            }
            if (child.TryGetComponent(out Collider2D collider))
            {
                collider.enabled = false;
            }
        }
        Encounter encounter = Encounters[Random.Range(0, Encounters.Count)];

    }
    protected GameObject InstantiateEncounter(Encounter encounter)
    {
        GameObject newEncounter = Instantiate(encounter.gameObject, transform.position, Quaternion.identity);
        return newEncounter;
    }
}

