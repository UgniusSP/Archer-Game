using UnityEngine;
using Utils;

public class BossSpawner : MonoBehaviour
{
    
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Transform spawnPoint;

    private bool _bossSpawned = false;
    
    void Update()
    {
        
        if(!_bossSpawned && GameManager.Instance.GetPoints() >= ProgressBar.Instance.GetMaxPoints())
        {
            SpawnBoss();
            _bossSpawned = true;
        }
    }
    
    private void SpawnBoss()
    {
        Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
