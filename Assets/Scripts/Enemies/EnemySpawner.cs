using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private float spawnInterval;

        private void Start()
        {
            BeginSpawning();
        }

        private void BeginSpawning()
        {
            StartCoroutine(SpawnEnemies());
        }

        private IEnumerator SpawnEnemies()
        {
            while (ShouldContinueSpawning())
            {
                yield return WaitBeforeNextSpawn();
                ExecuteSpawn();
            }
        }

        private bool ShouldContinueSpawning()
        {
            return true;
        }

        private WaitForSeconds WaitBeforeNextSpawn()
        {
            return new WaitForSeconds(spawnInterval);
        }

        private void ExecuteSpawn()
        {
            SpawnEnemyAtRandomLocation();
        }

        private void SpawnEnemyAtRandomLocation()
        {
            int spawnIndex = GetRandomSpawnIndex();
            InstantiateEnemyAt(spawnPoints[spawnIndex]);
        }

        private int GetRandomSpawnIndex()
        {
            return Random.Range(0, spawnPoints.Length);
        }

        private void InstantiateEnemyAt(Transform spawnPoint)
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}