using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AnimationManager : MonoBehaviour
{
    [Header("Animators")]
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimatorOverrideController _animatorOverrideController;

    [Header("Scripts")]
    [SerializeField] private Weapon weapon;
    [SerializeField] private WeaponAmmo weaponAmmo;
    [SerializeField] private KeyboardInput keyboardInput;
    [SerializeField] private RaycastAttack raycastAttack;

    [Header("Rig")]
    [SerializeField] private MultiAimConstraint _rHandAim;
    [SerializeField] private TwoBoneIKConstraint _lHandIk;

    [Header("Values")]
    [SerializeField] private float _durationRig;
    [SerializeField] private float _animationSpeed;

    private Animation _animation;

    private void OnEnable()
    {
        _animator.runtimeAnimatorController = _animatorOverrideController;
        _animator.SetFloat("speedShoting", _animationSpeed);

        keyboardInput.OnAimStarted += AimAnimationStart;
        keyboardInput.OnAimEnded += AimAnimationEnd;
        Weapon.OnAttackStarted += AttackAnimationStart;
        weapon.OnAttackEnded += AttackAnimationEnd;
        weaponAmmo.OnReloadStarted += ReloadAnimationStart;
        WeaponAmmo.OnReloadEnded += ReloadAinmationEnd;
        WeaponSwitching.OnWeaponChange += WeaponChangeAnimationStart;
        WeaponSwitching.OnWeaponChanged += WeaponChangeAnimationEnd;
    }
    private void OnDisable()
    {
        keyboardInput.OnAimStarted -= AimAnimationStart;
        keyboardInput.OnAimEnded -= AimAnimationEnd;
        Weapon.OnAttackStarted -= AttackAnimationStart;
        weapon.OnAttackEnded -= AttackAnimationEnd;
        weaponAmmo.OnReloadStarted -= ReloadAnimationStart;
        WeaponAmmo.OnReloadEnded -= ReloadAinmationEnd;
        WeaponSwitching.OnWeaponChange -= WeaponChangeAnimationStart;
        WeaponSwitching.OnWeaponChanged -= WeaponChangeAnimationEnd;
    }

    private void AimAnimationStart()
    {
        _animator.SetBool("aim", true);
        raycastAttack.spreadFactor = raycastAttack.spreadFactorMax;
    }

    private void AimAnimationEnd()
    {
        _animator.SetBool("aim", false);
        raycastAttack.spreadFactor = raycastAttack.spreadFactorMin;
    }

    private void AttackAnimationStart()
    {
        _animator.SetBool("shoot", true);
    }

    private void AttackAnimationEnd()
    {
        _animator.SetBool("shoot", false);
    }

    private void ReloadAnimationStart()
    {
        _animator.SetTrigger("reload");
        DecreaseRigArm();
    }

    private void ReloadAinmationEnd()
    {
        _animator.ResetTrigger("reload");
        StartCoroutine(IncreaseRigArm(_durationRig));
    }

    private void WeaponChangeAnimationStart()
    {
        _animator.SetTrigger("changeWeapon");
        DecreaseRigArm();
    }

    private void WeaponChangeAnimationEnd()
    {
        _animator.ResetTrigger("changeWeapon");
        StartCoroutine(IncreaseRigArm(_durationRig));
    }

    IEnumerator IncreaseRigArm(float durationRig)
    {
        float time = 0;
        float startValue = 0;
        float endValue = 1f;
        while(time < _durationRig)
        {
            _rHandAim.weight = Mathf.Lerp(startValue, endValue, time / durationRig);
            _lHandIk.weight = Mathf.Lerp(startValue, endValue, time / durationRig);
            time += Time.deltaTime;
            yield return null;
        }
        startValue = endValue;
    }
    private void DecreaseRigArm()
    {
        _rHandAim.weight = 0;
        _lHandIk.weight = 0;
    }
}
