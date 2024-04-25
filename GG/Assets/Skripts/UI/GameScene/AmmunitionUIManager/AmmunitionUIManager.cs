using TMPro;
using UnityEngine;

public class AmmunitionUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _magazineSizeText;
    [SerializeField] private TextMeshProUGUI _magazineCountText;

    private WeaponAmmo _weaponammo;

    private void OnEnable()
    {
        WeaponSwitching.OnUpdateWeaponInfo += GetAmmoValue;
        Weapon.OnAttackStarted += CallUpdateMethod;
        WeaponAmmo.OnReloadEnded += CallUpdateMethod;
        WeaponAmmo.OnAmmoRefill += CallUpdateMethod;
    }

    private void OnDisable()
    {
        WeaponSwitching.OnUpdateWeaponInfo -= GetAmmoValue;
        Weapon.OnAttackStarted -= CallUpdateMethod;
        WeaponAmmo.OnReloadEnded -= CallUpdateMethod;
        WeaponAmmo.OnAmmoRefill -= CallUpdateMethod;
    }

    private void CallUpdateMethod()
    {
        UpdateAmmoInfo(_weaponammo.currentAmmo, _weaponammo.currentMaxAmmo);
    }

    private void GetAmmoValue(WeaponAmmo weaponAmmo)
    {
        if (weaponAmmo != null)
        {
            _weaponammo = weaponAmmo;

            CallUpdateMethod();
        }
    }

    private void UpdateAmmoInfo(int magazineSize, int magazineCount)
    {
        _magazineSizeText.text = magazineSize.ToString();
        _magazineCountText.text = magazineCount.ToString();
    }
}
