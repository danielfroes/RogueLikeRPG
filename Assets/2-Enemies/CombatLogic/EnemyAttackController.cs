using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DirectionSystem;
using Squeak;

namespace Enemy
{
    // Controla os ataques do inimigo
    public class EnemyAttackController : MonoBehaviour
    {
        [SerializeField] private EnemyAttackData[] enemyAttacks = null;
        private Transform _playerTransform = null;
        private GameObject _attackContainer = null;
        private IEnumerator _spawnAttacksCoroutine;


        private void Awake()
        {
            _playerTransform = FindObjectOfType<PlayerController>().gameObject.transform;
            _spawnAttacksCoroutine = SpawnAttacks();
        }

        private void Start()
        {
            StartAttacks();
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

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                StopAttacks();
            }
        }
        

        //invoke the attacks
        private IEnumerator SpawnAttacks()
        {
            while(true)
            {
                int rand = Random.Range(0, enemyAttacks.Length);
                EnemyAttackData attack = Instantiate<EnemyAttackData>(enemyAttacks[rand], _attackContainer.transform);
                //Fetch a random possible direction for the attack
                Direction attackDir = attack.GetRandomPossibleDirection();
                //fix position
                AttackTransformFixer.FixPosition(attack, attackDir,_playerTransform.position);
                AttackTransformFixer.FixRotation(attack, attackDir);

                yield return new WaitForSeconds(attack.timeToNextAttack);
            }

        }

       



    }
}