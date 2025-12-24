using UnityEngine;

namespace TappyTale
{
    public sealed class ContactDamage : MonoBehaviour
    {
        [SerializeField] private float damagePerSecond = 2f;

        private void OnCollisionStay(Collision collision)
        {
            // Purpose: Deal steady damage while touching the player.
            if (!collision.collider.CompareTag("Player")) return;

            if (collision.collider.TryGetComponent<Health>(out var health))
                health.ApplyDamage(damagePerSecond * Time.fixedDeltaTime);
        }
    }
}
