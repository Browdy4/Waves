using UnityEngine;

public class GrenadeAmmo : MonoBehaviour
{
    [SerializeField] private int _maxGrenadeAmmo;

    [HideInInspector] public int CurrentGrenadeAmmo;

    private void Start()
    {
        CurrentGrenadeAmmo = _maxGrenadeAmmo;
    }
    private void OnEnable()
    {
        AmmoBag.OnAmmoSelected += AmmoRefill;
    }
    private void OnDisable()
    {
        AmmoBag.OnAmmoSelected -= AmmoRefill;
    }

    private void AmmoRefill()
    {
        CurrentGrenadeAmmo = _maxGrenadeAmmo;
    }
}
