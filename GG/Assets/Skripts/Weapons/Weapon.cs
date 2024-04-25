using System;
using System.Threading.Tasks;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HideInInspector] public bool attackInProcess = false;

    public static event Action OnAttackStarted;
    public event Action OnEmptyClip;
    public event Action OnAttackEnded;

    [SerializeField] private WeaponConfig weaponConfig;

    [Header("Scripts")]
    [SerializeField] WeaponAmmo weaponAmmo;
    [SerializeField] RaycastAttack raycastAttack;

    private bool _semiAuto => weaponConfig.SemiAuto;
    private float _coolDownAttack => weaponConfig.CoolDownAttack;

    private bool _isPaused => Time.timeScale == 0f;

    private bool _canShoot = true;

    private void OnEnable()
    {
        WeaponSwitching.OnWeaponChange += BlockAttack;
        WeaponSwitching.OnWeaponChanged += OpenAttack;
    }

    private void OnDisable()
    {
        WeaponSwitching.OnWeaponChange -= BlockAttack;
        WeaponSwitching.OnWeaponChanged -= OpenAttack;
    }

    private void Update()
    {
        Run();
    }

    private void Run()
    {
        if (_semiAuto)
        {
            if (Input.GetMouseButtonDown(0))
                StartAttack();
        }
        else
        {
            if (Input.GetMouseButton(0))
                StartAttack();
        }
    }

    private async void StartAttack()
    {
        if (!_canShoot || weaponAmmo.reloadInProcess || _isPaused) return;

        if (weaponAmmo.currentAmmo != 0)
        {
            AttackStarted();

            raycastAttack.PerformAttack();

            await CoolDown(_coolDownAttack);

            AttackEnded();
        }
        else
            if(Input.GetMouseButtonDown(0))
            OnEmptyClip?.Invoke();
    }

    private void BlockAttack()
    {
        _canShoot = false;
    }

    private void OpenAttack()
    {
        _canShoot = true;
    }

    private void AttackStarted()
    {
        attackInProcess = true;

        weaponAmmo.currentAmmo--;

        OnAttackStarted?.Invoke();
    }

    private void AttackEnded()
    {
        OnAttackEnded?.Invoke();
        attackInProcess = false;
    }

    private async Task CoolDown(float waitTime)
    {
        _canShoot = false;
        await Task.Delay(TimeSpan.FromSeconds(waitTime));
        _canShoot = true;
    }
}   