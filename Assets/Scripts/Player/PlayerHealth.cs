using System;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float _health;
        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private float _damage;

        public float HealthDelta { get; private set; }

        public event Action<float> OnTakeDamage;

        private void Start()
        {
            _health = _maxHealth;
            HealthDelta = _health / _maxHealth;
        }

        [ContextMenu("Damage")]
        private void TakeDamage()
        {
            _health -= _damage;
            _health = Mathf.Clamp(_health, 0, _maxHealth);
            HealthDelta = _health / _maxHealth;

            OnTakeDamage?.Invoke(_health);
        }
    }
}