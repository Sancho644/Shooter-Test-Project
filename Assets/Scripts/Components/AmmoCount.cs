using UnityEngine;

namespace Components
{
    public class AmmoCount : MonoBehaviour
    {
        [SerializeField] private int _maxAmmo;

        private int _ammoValue;

        public int MaxAmmo => _maxAmmo;

        public delegate void OnChangeAmmo(int currentAmmo);
        public OnChangeAmmo OnChange;

        private void Start()
        {
            if (_ammoValue == 0)
            {
                _ammoValue = _maxAmmo;
                OnChange?.Invoke(_ammoValue);
            }
        }

        public void SetAmmoCount(int value)
        {
            if (_ammoValue <= 0)
            {
                _ammoValue = _maxAmmo;
            }
            else
            {
                _ammoValue -= value;
            }

            OnChange?.Invoke(_ammoValue);
        }
    }
}