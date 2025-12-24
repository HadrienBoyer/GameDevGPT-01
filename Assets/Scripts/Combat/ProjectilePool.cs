using System.Collections.Generic;
using UnityEngine;

namespace TappyTale
{
    public sealed class ProjectilePool : MonoBehaviour
    {
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private int prewarm = 32;

        private readonly List<Projectile> _pool = new();

        private void Awake()
        {
            Prewarm();
        }

        // Purpose: Avoid runtime spikes by instantiating a baseline number of projectiles.
        private void Prewarm()
        {
            if (!projectilePrefab) return;

            for (int i = 0; i < prewarm; i++)
                CreateOneInactive();
        }

        private Projectile CreateOneInactive()
        {
            Projectile p = Instantiate(projectilePrefab, transform);
            p.gameObject.SetActive(false);
            _pool.Add(p);
            return p;
        }

        // Purpose: Fetch an inactive projectile or create one if needed.
        public Projectile Get()
        {
            for (int i = 0; i < _pool.Count; i++)
            {
                if (!_pool[i].gameObject.activeSelf)
                    return _pool[i];
            }
            return CreateOneInactive();
        }
    }
}
