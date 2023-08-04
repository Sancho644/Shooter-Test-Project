using UnityEngine;

namespace Components
{
    public class BulletProjectile : MonoBehaviour
    {
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _destroyDelay;
        [SerializeField] private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody.AddForce(transform.forward * _bulletSpeed);
            Destroy(gameObject, _destroyDelay);
        }

        private void OnCollisionEnter(Collision other)
        {
            Destroy(gameObject);
        }
    }
}