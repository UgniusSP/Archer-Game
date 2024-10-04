using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private float spawnRange;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float heartValue;
    private Transform _player;
    
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    //todo spawn heart at random location
    
    public float GetHeartValue()
    {
        return heartValue;
    }
    
}
