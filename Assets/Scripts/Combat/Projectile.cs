using UnityEngine;

namespace TappyTale
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public sealed class Projectile : MonoBehaviour
    {
        [SerializeField] private float defaultDamage = 1f;

        private Rigidbody _rb;
        private float _damage;
        private float _dieAtTime;
        private int _ownerInstanceId;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.useGravity = false;
            _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }

        // Purpose: Initialize projectile for reuse (pool-friendly).
        public void Launch(Vector3 velocity, float damage, float lifetime, int ownerInstanceId)
        {
            _ownerInstanceId = ownerInstanceId;
            _damage = damage <= 0f ? defaultDamage : damage;
            _dieAtTime = Time.time + Mathf.Max(0.05f, lifetime);

            transform.forward = velocity.sqrMagnitude > 0.001f ? velocity.normalized : transform.forward;

            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            _rb.linearVelocity = velocity;

            gameObject.SetActive(true);
        }

        private void Update()
        {
            if (Time.time >= _dieAtTime)
                gameObject.SetActive(false);
        }

        private void OnCollisionEnter(Collision collision)
        {
            // Purpose: Apply damage on impact, then disable self.
            if (collision.collider.attachedRigidbody && collision.collider.attachedRigidbody.GetInstanceID() == _ownerInstanceId)
                return;

            if (collision.collider.TryGetComponent<Health>(out var health))
                health.ApplyDamage(_damage);

            gameObject.SetActive(false);
        }
    }
}
