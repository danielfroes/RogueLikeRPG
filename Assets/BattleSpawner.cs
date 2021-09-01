using System;
using System.Collections.Generic;
using Enemy;

using UnityEngine;
using Random = UnityEngine.Random;


public class BattleSpawner : MonoBehaviour
{
    [SerializeField] EnemyAttackSpawnerController _attackSpawnerController;
    [SerializeField] List<EnemyAttackSpawner> _spawnableEnemies = new List<EnemyAttackSpawner>();

    [SerializeField] float offsetBetweenEnemies = 2 ;


    void Awake()
    {
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        var rand = Random.Range(2, 4);
        var positionToSpawn = Vector3.left * offsetBetweenEnemies * (rand - 1)/ 2;
        
        for (var i = 0; i < rand; i++)
        {
            var randomEnemyIndex = Random.Range(0, _spawnableEnemies.Count);

            var randomEnemy = Instantiate(_spawnableEnemies[randomEnemyIndex], transform);
            
            randomEnemy.transform.localPosition = positionToSpawn;
            positionToSpawn += Vector3.right * offsetBetweenEnemies;
            
            randomEnemy.Setup(_attackSpawnerController);
        }

    }


}
