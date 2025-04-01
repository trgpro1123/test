using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave 
{
    public List<EnemySpawnData> enemies = new List<EnemySpawnData>();
    public List<GameObject> specialEnemies = new List<GameObject>();
}
