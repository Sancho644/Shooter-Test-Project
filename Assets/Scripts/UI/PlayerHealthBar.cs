using System;
using System.Globalization;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerHealthBar : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private Image _frontHealthBar;
        [SerializeField] private Image _backHealthBar;
        [SerializeField] private Text _currentHealthText;

        private float _lerpTimer;
        private bool _damageAnimation;
        
        private const float CHIP_SPEED = 10f;

        private void Start()
        {
            _playerHealth.OnTakeDamage += OnDamage;
        }

        private void OnDamage(float health)
        {
            _lerpTimer = 0f;
            _damageAnimation = true;
            _currentHealthText.text = Math.Abs(health).ToString(CultureInfo.InvariantCulture);
        }

        private void Update()
        {
            if (_damageAnimation)
                UpdateHealthUI();
        }

        private void UpdateHealthUI()
        {
            var fillBack = _backHealthBar.fillAmount;
            var healthDelta = _playerHealth.HealthDelta;

            if (fillBack > healthDelta)
            {
                _frontHealthBar.fillAmount = healthDelta;
                _lerpTimer += Time.deltaTime;
                var percentComplete = _lerpTimer / CHIP_SPEED;
                percentComplete *= percentComplete;
                _backHealthBar.fillAmount = Mathf.Lerp(fillBack, healthDelta, percentComplete);
            }

            _damageAnimation = _frontHealthBar.fillAmount < _backHealthBar.fillAmount;
        }
    }
}