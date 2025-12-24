using UnityEngine;
using UnityEngine.SceneManagement;

namespace TappyTale
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField] private Health playerHealth;

        private void Awake()
        {
            if (!playerHealth)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player) playerHealth = player.GetComponent<Health>();
            }
        }

        private void OnEnable()
        {
            if (playerHealth) playerHealth.Died += OnPlayerDied;
        }

        private void OnDisable()
        {
            if (playerHealth) playerHealth.Died -= OnPlayerDied;
        }

        // Purpose: Minimal loop—restart scene on death.
        private void OnPlayerDied()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
