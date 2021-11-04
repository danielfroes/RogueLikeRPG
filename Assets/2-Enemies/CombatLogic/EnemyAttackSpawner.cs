using System;
using System.Collections;
using System.Collections.Generic;
using Squeak;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyAttackSpawner:MonoBehaviour
    {
        [SerializeField] List<EnemyAttackData> _enemyAttacks = new List<EnemyAttackData>();
        [SerializeField] int attacksToTaunt = 4;
        [SerializeField] Animator _animator = null;
        [SerializeField] EnemyStatusController _statusController;
        //[SerializeField] EnemyAttackSpawnerController _spawnerController;
        GameObject _attackContainer = null;
        int cntToTaunt = 0;
        Transform _playerTransform;
        void Awake()
        {
            _attackContainer = new GameObject();
            _attackContainer.name = "[EnemyAttacksContainer-RunModeOnly]";

            Instantiate(_attackContainer, transform);
            
            _playerTransform = FindObjectOfType<PlayerController>().transform;

            //_spawnerController.RegisterAttackSpawner(this);
            //_statusController.OnDeathEvent += (() => _spawnerController.UnregisterAttackSpawner(this));

        }


        static readonly int TauntTriggerID = Animator.StringToHash("Taunt");
        public IEnumerator SpawnSelectedEnemyAttack(float timeToSubtract)
        {
            var rand = Random.Range(0, _enemyAttacks.Count);

            var attackData = _enemyAttacks[rand];
            for (var i = 0; i <= attackData.numberSuccessiveAttacks; i++)
            {
                CreateAttack(attackData);
                yield return new WaitForSeconds(attackData.timeBetweenSuccessiveAttacks);
            }

            //TODO: Gambiarra (talvez colocar todas os triggers de animacao em uma classe separada, ou no state machine do inimigo)
            if (cntToTaunt++ % attacksToTaunt == 0)
            {
                _animator.SetTrigger(TauntTriggerID);
            }

            yield return new WaitForSeconds(attackData.timeToNextAttack - timeToSubtract);
        }

        void CreateAttack(EnemyAttackData attackPrefab)
        {
            var attack = Instantiate(attackPrefab, _attackContainer.transform);
            //Fetch a random possible direction for the attack
            var attackDir = attack.GetRandomPossibleDirection();
            //fix position
            AttackTransformFixer.FixPosition(attack, attackDir, _playerTransform.position);
            AttackTransformFixer.FixRotation(attack, attackDir); 
        }
        
    }
}