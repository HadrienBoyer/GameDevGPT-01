using UnityEngine;

namespace TappyTale
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class EnemyChaser : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 4.5f;
        [SerializeField] private float turnSpeed = 12f;
        [SerializeField] private float stoppingDistance = 1.6f;

        private Rigidbody _rb;
        private Transform _target;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.interpolation = RigidbodyInterpolation.Interpolate;
        }

        private void Start()
        {
            // Purpose: Find player once without needing extra wiring.
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            _target = player ? player.transform : null;
        }

        private void FixedUpdate()
        {
            if (!_target) return;

            Vector3 to = _target.position - transform.position;
            to.y = 0f;

            float dist = to.magnitude;
            if (dist <= stoppingDistance)
            {
                _rb.linearVelocity = new Vector3(0f, _rb.linearVelocity.y, 0f);
                return;
            }

            Vector3 dir = to / Mathf.Max(0.001f, dist);
            Vector3 desired = dir * moveSpeed;

            _rb.linearVelocity = new Vector3(desired.x, _rb.linearVelocity.y, desired.z);

            if (dir.sqrMagnitude > 0.001f)
            {
                Quaternion desiredRot = Quaternion.LookRotation(dir, Vector3.up);
                _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, desiredRot, turnSpeed * Time.fixedDeltaTime));
            }
        }
    }
}
