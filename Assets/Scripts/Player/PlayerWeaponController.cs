using UnityEngine;

namespace TappyTale
{
    public sealed class PlayerWeaponController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlayerAim aim;
        [SerializeField] private Transform muzzle;
        [SerializeField] private ProjectilePool projectilePool;

        [Header("Weapon")]
        [SerializeField] private Weapon weapon = new Weapon();

        private float _nextShotTime;

        private void Reset()
        {
            aim = GetComponent<PlayerAim>();
        }

        private void Update()
        {
            if (!aim || !muzzle || !projectilePool) return;

            bool wantsFire = Input.GetMouseButton(0);
            if (!wantsFire) return;

            if (Time.time < _nextShotTime) return;
            _nextShotTime = Time.time + weapon.SecondsPerShot;

            Fire();
        }

        // Purpose: Spawn and launch a projectile using current aim direction.
        private void Fire()
        {
            Vector3 dir = (aim.AimPoint - muzzle.position);
            dir.y = 0f;

            if (dir.sqrMagnitude < 0.001f)
                dir = transform.forward;

            dir = ApplySpread(dir.normalized, weapon.spreadDegrees);

            Projectile p = projectilePool.Get();
            p.transform.position = muzzle.position;

            Vector3 vel = dir * weapon.projectileSpeed;
            p.Launch(vel, weapon.damage, weapon.projectileLifetime, ownerInstanceId: GetInstanceID());
        }

        // Purpose: Add controllable inaccuracy for better feel.
        private static Vector3 ApplySpread(Vector3 direction, float degrees)
        {
            if (degrees <= 0f) return direction;

            float yaw = Random.Range(-degrees, degrees);
            Quaternion q = Quaternion.AngleAxis(yaw, Vector3.up);
            return (q * direction).normalized;
        }
    }
}
