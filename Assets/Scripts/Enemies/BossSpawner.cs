using UnityEngine;
using Utils;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Transform spawnPoint;
    
    private bool _bossSpawned = false;

    void Update()
    {
        CheckBossSpawnCondition();
    }

    private void CheckBossSpawnCondition()
    {
        if (ShouldSpawnBoss())
        {
            SpawnBoss();
            MarkBossAsSpawned();
        }
    }

    private bool ShouldSpawnBoss()
    {
        return !_bossSpawned && HasReachedMaxPoints();
    }

    private bool HasReachedMaxPoints()
    {
        return GameManager.Instance.GetPoints() >= ProgressBar.Instance.GetMaxPoints();
    }

    private void SpawnBoss()
    {
        Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    private void MarkBossAsSpawned()
    {
        _bossSpawned = true;
    }
}