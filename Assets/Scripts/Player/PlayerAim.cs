using UnityEngine;

namespace TappyTale
{
    public sealed class PlayerAim : MonoBehaviour
    {
        [Header("Aiming")]
        [SerializeField] private Camera worldCamera;
        [SerializeField] private LayerMask aimMask = ~0; // e.g. Floor layer
        [SerializeField] private float turnSpeed = 25f;

        public Vector3 AimPoint { get; private set; }

        private void Reset()
        {
            worldCamera = Camera.main;
        }

        private void Update()
        {
            if (!worldCamera) worldCamera = Camera.main;
            if (!worldCamera) return;

            // Purpose: Aim using a ray to the ground.
            Ray ray = worldCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 500f, aimMask, QueryTriggerInteraction.Ignore))
            {
                AimPoint = hit.point;

                Vector3 to = AimPoint - transform.position;
                to.y = 0f;

                if (to.sqrMagnitude > 0.001f)
                {
                    Quaternion desired = Quaternion.LookRotation(to.normalized, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, desired, turnSpeed * Time.deltaTime);
                }
            }
        }
    }
}
