using Components;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AmmoCountController : MonoBehaviour
    {
        [SerializeField] private Text _maxAmmo;
        [SerializeField] private Text _currentAmmo;
        [SerializeField] private AmmoCount _ammoCount;

        private void Start()
        {
            _maxAmmo.text = _ammoCount.MaxAmmo.ToString();
            _ammoCount.OnChange += SetAmmo;
        }

        private void SetAmmo(int value)
        {
            _currentAmmo.text = value.ToString();
        }
    }
}