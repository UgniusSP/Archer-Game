using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Heart : MonoBehaviour
{
    
    [SerializeField] private float heartValue;
    
    private float _timeUntilSpawn;
    
    void Start()
    {
        
        Destroy(gameObject, 5f);
    }
    
    public float GetHeartValue()
    {
        return heartValue;
    }
    
}
