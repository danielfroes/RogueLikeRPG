using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    // Controla os ataques do inimigo
    public class EnemyAttackSpawnerController : MonoBehaviour
    {
        [SerializeField] BattleStateController _battleStateController;
        [SerializeField] float _timeToWaitSubtractionPerEnemy = 0.2f;
        
        //List<EnemyAttackSpawner> _enemiesAttackSpawners = new List<EnemyAttackSpawner>();

        
        IEnumerator _spawnAttacksCoroutine;
        
        //void Awake()
        //{
            
        //}

        public void StartAttacks()
        {
            _spawnAttacksCoroutine = SelectEnemyToAttack();
            StartCoroutine(_spawnAttacksCoroutine);
        }

        public void StopAttacks()
        {
            StopCoroutine(_spawnAttacksCoroutine);
        }

        /*public void RegisterAttackSpawner(EnemyAttackSpawner attackSpawner)
        {
            _enemiesAttackSpawners.Add(attackSpawner);
        }

        public void UnregisterAttackSpawner(EnemyAttackSpawner attackSpawner)
        {
            _enemiesAttackSpawners.Remove(attackSpawner);
        }
        */
        IEnumerator SelectEnemyToAttack()
        {
            yield return new WaitForSeconds(2);
            while(true)
            {
                var spawnedEnemies = _battleStateController.SpawnedEnemies;
                var enemyQuantity = spawnedEnemies.Count;
                var rand = Random.Range(0, enemyQuantity);
                var totalTimeSubtracted = _timeToWaitSubtractionPerEnemy * enemyQuantity - 1;

                yield return spawnedEnemies[rand].GetComponent<EnemyAttackSpawner>().SpawnSelectedEnemyAttack(totalTimeSubtracted);
                
                /*var enemyQuantity = _enemiesAttackSpawners.Count;
                var rand = Random.Range(0, enemyQuantity);
                var totalTimeSubtracted = _timeToWaitSubtractionPerEnemy * enemyQuantity - 1;

                yield return _enemiesAttackSpawners[rand].SpawnSelectedEnemyAttack(totalTimeSubtracted);
                */
            }

        }


       
        



    }
}