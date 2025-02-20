
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "RoundRobinMultiAttacker", menuName = "WeaponAttackers/RoundRobinMultiAttacker")]
public class RoundRobinMultiAttacker : AMultipleWeaponAttacker
{
    protected override int PickNextAttackerIndex()
    {
        return (attackerIndex + 1) % weaponAttackers.Count;
    }
}

