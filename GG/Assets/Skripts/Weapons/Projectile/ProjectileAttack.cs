using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    [SerializeField] private GrenadeAttack _projectilePrefab;
    [SerializeField] private KeyboardInput _keyboardInput;
    [SerializeField] private GrenadeAmmo _grenadeAmmo;

    [SerializeField] private Transform _weaponMuzzle;
    [SerializeField] private ForceMode _forceMode = ForceMode.Impulse;
    [SerializeField, Min(0f)] private float _force = 5f;

    private void OnEnable()
    {
        _keyboardInput.OnThrowGrenade += PerformAttack;
    }

    private void OnDisable()
    {
        _keyboardInput.OnThrowGrenade -= PerformAttack;
    }

    private void PerformAttack()
    {
        if (_grenadeAmmo.CurrentGrenadeAmmo <= 0)
            return;

        _grenadeAmmo.CurrentGrenadeAmmo--;

        var projectile = Instantiate(_projectilePrefab, _weaponMuzzle.position, _weaponMuzzle.rotation);

        projectile.Rigidbody.AddForce(_weaponMuzzle.forward * _force,_forceMode);
    }
}
