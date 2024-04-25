using System;
using System.Threading.Tasks;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public static event Action<WeaponAmmo> OnUpdateWeaponInfo;

    public static event Action OnWeaponChange;
    public static event Action OnWeaponChanged;

    [Header("References")]
    [SerializeField] private Transform[] weapons;

    [Header("Keys")]
    [SerializeField] private KeyCode[] keys;

    [Header("Settings")]
    [SerializeField] private float switchTime;

    private bool _isPaused => Time.timeScale == 0f;

    private int selectedWeapon;
    private float timeSinceLastSwitch;
    private WeaponAmmo weaponAmmo;
    private Weapon weaponAttack;

    private void Start()
    {
        SetWeapons();
        Select(selectedWeapon);

        timeSinceLastSwitch = 0f;
    }

    private void Update()
    {
        if (_isPaused)
            return;

        if (weaponAmmo.reloadInProcess == false && weaponAttack.attackInProcess == false)
        {
            int previousSelectedWeapon = selectedWeapon;

            for (int i = 0; i < keys.Length; i++)
            {
                if (Input.GetKeyDown(keys[i]) && timeSinceLastSwitch >= switchTime)
                    selectedWeapon = i;
            }

            if (previousSelectedWeapon != selectedWeapon) Select(selectedWeapon);
        }
        timeSinceLastSwitch += Time.deltaTime;
    }

    private void SetWeapons()
    {
        weapons = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            weapons[i] = transform.GetChild(i);
        }

        if(keys == null) keys = new KeyCode[weapons.Length];
    }
    private void Select(int weaponIndex)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(i == weaponIndex);

            weaponAttack = weapons[weaponIndex].gameObject.GetComponent<Weapon>();
            weaponAmmo = weapons[weaponIndex].gameObject.GetComponent<WeaponAmmo>();
        }
        timeSinceLastSwitch = 0f;

        UpdateWeaponInfo();

        ChangeWEaponEvent();
    }

    private void UpdateWeaponInfo()
    {
        OnUpdateWeaponInfo?.Invoke(weaponAmmo);
    }

    private async void ChangeWEaponEvent()
    {
        OnWeaponChange?.Invoke();

        await CoolDown();

        OnWeaponChanged?.Invoke();
    }

    private async Task CoolDown()
    {
        await Task.Delay(TimeSpan.FromSeconds(switchTime));
    }
}
