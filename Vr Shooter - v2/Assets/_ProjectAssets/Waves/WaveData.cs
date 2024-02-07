using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Data", menuName = "Waves/Wave Data", order = 1)]
public class Wave : ScriptableObject
{
    [field: SerializeField]
    public GameObject[] EnemiesInWave { get; private set; }
    
    [field: SerializeField]
    public float TimeBeforeThisWave { get; private set; }
    
    [field: SerializeField]
    public float TimeBetweenSpawnEnemiesInsideWave { get; private set; }
    
    [field: SerializeField]
    public float NumberToSpawn { get; private set; }
}