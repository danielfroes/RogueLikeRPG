using System.Collections.Generic;
using Squeak;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<EnemyStatusController> _spawnableEnemies = new List<EnemyStatusController>();

    [SerializeField] float offsetBetweenEnemies = 2 ;


    public List<EnemyStatusController> SpawnEnemies()
    {
        var rand = Random.Range(2, 4);
        var positionToSpawn = Vector3.left * offsetBetweenEnemies * (rand - 1)/ 2;
        var spawnedEnemies = new List<EnemyStatusController>();

        for (var i = 0; i < rand; i++)
        {
            var randomEnemyIndex = Random.Range(0, _spawnableEnemies.Count);

            var randomEnemy = Instantiate(_spawnableEnemies[randomEnemyIndex], transform);
            
            randomEnemy.transform.localPosition = positionToSpawn;
            positionToSpawn += Vector3.right * offsetBetweenEnemies;
            
            spawnedEnemies.Add(randomEnemy);
        }

        return spawnedEnemies;
    }


}
