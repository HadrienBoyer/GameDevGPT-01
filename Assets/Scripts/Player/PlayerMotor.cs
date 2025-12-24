using UnityEngine;

namespace TappyTale
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class PlayerMotor : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 8f;
        [SerializeField] private float acceleration = 40f;

        private Rigidbody _rb;
        private Vector3 _desiredVelocity;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.interpolation = RigidbodyInterpolation.Interpolate;
        }

        private void Update()
        {
            // Purpose: Read input in Update for responsiveness.
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            Vector3 input = new Vector3(x, 0f, z);
            input = Vector3.ClampMagnitude(input, 1f);

            _desiredVelocity = input * moveSpeed;
        }

        private void FixedUpdate()
        {
            // Purpose: Apply movement via physics for stable collisions.
            Vector3 current = _rb.linearVelocity;
            Vector3 target = new Vector3(_desiredVelocity.x, current.y, _desiredVelocity.z);

            Vector3 delta = target - current;
            float maxDelta = acceleration * Time.fixedDeltaTime;

            if (delta.sqrMagnitude > maxDelta * maxDelta)
                delta = delta.normalized * maxDelta;

            _rb.linearVelocity = current + delta;
        }
    }
}
