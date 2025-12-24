using UnityEngine;

namespace TappyTale
{
    [System.Serializable]
    public sealed class Weapon
    {
        [Header("Firing")]
        public float fireRate = 12f;         // shots/sec
        public float projectileSpeed = 22f;
        public float damage = 1f;
        public float projectileLifetime = 2f;

        [Header("Spread")]
        [Range(0f, 10f)] public float spreadDegrees = 1.5f;

        public float SecondsPerShot => fireRate <= 0f ? 0.2f : (1f / fireRate);
    }
}
