using UnityEngine;

namespace TappyTale
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform player;
        [SerializeField] private GameObject enemyPrefab;

        [Header("Spawning")]
        [SerializeField] private float spawnRadius = 18f;
        [SerializeField] private float minDistanceFromPlayer = 8f;
        [SerializeField] private float spawnPerSecond = 0.5f;
        [SerializeField] private int maxAlive = 25;

        private float _nextSpawnTime;

        private void Reset()
        {
            spawnPerSecond = 0.5f;
            maxAlive = 25;
        }

        private void Update()
        {
            if (!player || !enemyPrefab) return;
            if (Time.time < _nextSpawnTime) return;

            if (CountAliveEnemies() >= maxAlive) return;

            _nextSpawnTime = Time.time + (spawnPerSecond <= 0f ? 2f : (1f / spawnPerSecond));
            SpawnOne();
        }

        // Purpose: Spawn enemies around the player while keeping a minimum distance.
        private void SpawnOne()
        {
            Vector2 ring = Random.insideUnitCircle.normalized * Random.Range(minDistanceFromPlayer, spawnRadius);
            Vector3 pos = player.position + new Vector3(ring.x, 0f, ring.y);

            GameObject enemy = Instantiate(enemyPrefab, pos, Quaternion.identity);
            enemy.name = $"Enemy_{Time.frameCount}";
        }

        private static int CountAliveEnemies()
        {
            // Purpose: Simple cap without maintaining lists.
            return GameObject.FindGameObjectsWithTag("Enemy").Length;
        }
    }
}
