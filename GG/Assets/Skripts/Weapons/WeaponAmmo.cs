using System;
using System.Threading.Tasks;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    [SerializeField] WeaponConfig weaponConfig;

    [Header("Scripts")]
    [SerializeField] private Weapon weapon;
    [SerializeField] private KeyboardInput keyboardInput;

    [HideInInspector] public int currentAmmo;
    [HideInInspector] public int currentMaxAmmo;
    [HideInInspector] public bool reloadInProcess = false;

    private int _clipSize => weaponConfig.ClipSize;
    private int _maxAmmo => weaponConfig.MaxAmmo;
    private float _coolDownReload => weaponConfig.CoolDownReload;

    private bool _canReload = true;

    public event Action OnReloadStarted;
    public static event Action OnReloadEnded;
    public static event Action OnAmmoRefill;

    private void Awake()
    {
        currentAmmo = _clipSize;
        currentMaxAmmo = _maxAmmo;
    }

    private void OnEnable()
    {
        keyboardInput.OnReloadStarted += Reload;
        AmmoBag.OnAmmoSelected += AmmoRefill;
        WeaponSwitching.OnWeaponChange += BlockReload;
        WeaponSwitching.OnWeaponChanged += OpenReload;
    }
    private void OnDisable()
    {
        keyboardInput.OnReloadStarted -= Reload;
        AmmoBag.OnAmmoSelected += AmmoRefill;
        AmmoBag.OnAmmoSelected -= AmmoRefill;
        WeaponSwitching.OnWeaponChange -= BlockReload;
        WeaponSwitching.OnWeaponChanged -= OpenReload;
    }

    private async void Reload()
    {
        if (currentMaxAmmo <= 0) return;
        if (currentAmmo == _clipSize) return;

        if (!_canReload) return;

        ReloadStarted();

        await CoolDown(_coolDownReload);

        if (currentMaxAmmo > _clipSize)
        {
            currentMaxAmmo -= _clipSize - currentAmmo;
            currentAmmo = _clipSize;
        }
        else
        {
            if (currentMaxAmmo + currentAmmo > _clipSize)
            {
                currentMaxAmmo += currentAmmo - _clipSize;    
                currentAmmo = _clipSize;
            }
            else
            {
                currentAmmo += currentMaxAmmo;
                currentMaxAmmo = 0;
            }
        }

        ReloadEnded();
    }

    private void BlockReload()
    {
        _canReload = false;
    }

    private void OpenReload()
    {
        _canReload = true;
    }

    private void AmmoRefill()
    {
        currentMaxAmmo = _maxAmmo;

        Debug.Log($"патронов {currentMaxAmmo}");

        OnAmmoRefill?.Invoke();
    }

    private void ReloadStarted()
    {
        reloadInProcess = true;
        OnReloadStarted?.Invoke();
    }

    private void ReloadEnded()
    {
        OnReloadEnded?.Invoke();
        reloadInProcess = false;
        Debug.Log(currentMaxAmmo);
    }

    private async Task CoolDown(float waitTime)
    {
        _canReload = false;
        await Task.Delay(TimeSpan.FromSeconds(waitTime));
        _canReload = true;
    }
}
