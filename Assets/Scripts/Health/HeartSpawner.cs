using UnityEngine;

namespace Health
{
    public class HeartSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject heartPrefab;
        [SerializeField] private float maxSpawnTime;
        [SerializeField] private float minSpawnTime;
        [SerializeField] private float spawnRadius;
    
        private float _timeUntilSpawn;
        private Transform _player;

        private void Awake()
        {
            InitializePlayer();
            SetTimeUntilSpawn();
        }

        private void InitializePlayer()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void Update()
        {
            HandleHeartSpawning();
        }

        private void HandleHeartSpawning()
        {
            _timeUntilSpawn -= Time.deltaTime;

            if (_timeUntilSpawn <= 0)
            {
                SpawnHeart();
                SetTimeUntilSpawn();
            }
        }

        private void SpawnHeart()
        {
            Vector3 spawnPosition = CalculateSpawnPosition();
            Instantiate(heartPrefab, spawnPosition, Quaternion.identity);
        }

        private Vector3 CalculateSpawnPosition()
        {
            return _player.position + (Vector3)(Random.insideUnitCircle * spawnRadius);
        }

        private void SetTimeUntilSpawn()
        {
            _timeUntilSpawn = Random.Range(minSpawnTime, maxSpawnTime);
        }
    }
}