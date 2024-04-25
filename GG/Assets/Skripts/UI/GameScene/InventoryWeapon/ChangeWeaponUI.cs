using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWeaponUI : MonoBehaviour
{
    [Header("Keys")]
    [SerializeField] private KeyCode[] _keys;

    [Header("References")]
    [SerializeField] private Image[] _images;

    [Header("AlphaColorValue")]
    [SerializeField] private float _alphaColorValue;

    private int _previousSelectedWeapon;
    private int _selectedWeapon;

    private void OnEnable()
    {
        WeaponSwitching.OnWeaponChange += ChangeWeapon;
    }

    private void OnDisable()
    {
        WeaponSwitching.OnWeaponChange -= ChangeWeapon;
    }

    private void Start()
    {
        _images[0].color = ChangeColor(_alphaColorValue);
    }

    private void ChangeWeapon()
    {
        _previousSelectedWeapon = _selectedWeapon;

        _images[_previousSelectedWeapon].color = ChangeColor(0f);

        for (int i = 0; i < _keys.Length; i++)
        {
            if (Input.GetKeyDown(_keys[i]))
            {
                _images[i].color = ChangeColor(_alphaColorValue);
                _selectedWeapon = i;
            }
        }
    }

    private Color ChangeColor(float value)
    {
        return new Color(255, 255, 255, value);
    }
}
