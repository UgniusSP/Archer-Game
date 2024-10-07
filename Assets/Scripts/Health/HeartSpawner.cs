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
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            SetTimeUntilSpawn();
        }

        void Update()
        {
            _timeUntilSpawn -= Time.deltaTime;

            if (_timeUntilSpawn <= 0)
            {
                Vector3 spawnPosition = _player.position + (Vector3)(Random.insideUnitCircle * spawnRadius);
                Instantiate(heartPrefab, spawnPosition, Quaternion.identity);
                SetTimeUntilSpawn();
            }
        }

        private void SetTimeUntilSpawn()
        {
            _timeUntilSpawn = Random.Range(minSpawnTime, maxSpawnTime);
        }
    }
}
