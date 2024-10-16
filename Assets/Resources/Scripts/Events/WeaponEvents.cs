using UnityEngine.Events;

public class WeaponEvents
{
    public UnityEvent OnWeaponFired = new();
    public UnityEvent OnWeaponDropped = new();
    public UnityEvent OnWeaponReloaded = new();
    public UnityEvent OnWeaponSwitched = new();
    public UnityEvent OnWeaponPickedUp = new();
}
