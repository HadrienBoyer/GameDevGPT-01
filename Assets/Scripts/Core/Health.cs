using System;
using UnityEngine;

namespace TappyTale
{
    public sealed class Health : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 10f;

        public event Action<float, float> Changed; // (current, max)
        public event Action Died;

        public float Max => maxHealth;
        public float Current { get; private set; }

        private void Awake()
        {
            Current = maxHealth;
            Changed?.Invoke(Current, maxHealth);
        }

        // Purpose: Apply damage in a single guarded entry point.
        public void ApplyDamage(float amount)
        {
            if (amount <= 0f) return;
            if (Current <= 0f) return;

            Current = Mathf.Max(0f, Current - amount);
            Changed?.Invoke(Current, maxHealth);

            if (Current <= 0f)
                Died?.Invoke();
        }

        // Purpose: Restore health for pickups or resets.
        public void RestoreFull()
        {
            Current = maxHealth;
            Changed?.Invoke(Current, maxHealth);
        }
    }
}
