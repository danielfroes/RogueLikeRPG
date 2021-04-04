using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DirectionSystem;
using Squeak;
using Unity.Collections.LowLevel.Unsafe;

namespace Enemy
{
    // Controla os ataques do inimigo
    public class EnemyAttackController : MonoBehaviour
    {
        [SerializeField] private EnemyAttackData[] enemyAttacks = null;
        [SerializeField] int attacksToTaunt = 4;
        [SerializeField] Animator _animator = null;
        [SerializeField] Transform _playerTransform = null;
        private GameObject _attackContainer = null;
        private IEnumerator _spawnAttacksCoroutine; 
        
        static readonly int TauntTriggerID = Animator.StringToHash("Taunt");

        public static bool stun = false;
        
        private void Awake()
        {
            _spawnAttacksCoroutine = SpawnAttacks();
        }

        private void Start()
        {
            //StartAttacks();
        }

        public void StartAttacks()
        {
            _attackContainer = new GameObject();
            _attackContainer.name = "[EnemyAttacksContainer-RunModeOnly]";
            StartCoroutine(_spawnAttacksCoroutine);

        }

        public void StopAttacks()
        {
            StopCoroutine(_spawnAttacksCoroutine);
        }
        
        //invoke the attacks
        private IEnumerator SpawnAttacks()
        {

            var cnt = 0;
            yield return new WaitForSeconds(2);
            while(true)
            {
                var rand = Random.Range(0, enemyAttacks.Length);
                var attack = Instantiate<EnemyAttackData>(enemyAttacks[rand], _attackContainer.transform);
                //Fetch a random possible direction for the attack
                var attackDir = attack.GetRandomPossibleDirection();
                //fix position
                AttackTransformFixer.FixPosition(attack, attackDir,_playerTransform.position);
                AttackTransformFixer.FixRotation(attack, attackDir);

                //TODO: Gambiarra (talvez colocar todas os triggers de animacao em uma classe separada, ou no state machine do inimigo)
                if (cnt++ >= attacksToTaunt)
                {
                    cnt = 0;
                    _animator.SetTrigger(TauntTriggerID);
                }

                yield return new WaitForSeconds(attack.timeToNextAttack);
                
                if (stun)
                {
                    stun = false;
                    yield return new WaitForSeconds(2f);
                }
            }

        }
    }
}