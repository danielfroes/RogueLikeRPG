using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DirectionSystem;



namespace Enemy
{
    // Controla os ataques do inimigo
    public class EnemyAttackController : MonoBehaviour
    {
        [SerializeField] private float timeBtwAttacks = 3;
        [SerializeField] private EnemyAttackData[] enemyAttacks = null;
        private Transform _playerTransform = null;
        private GameObject _attackContainer = null;


        private void Awake()
        {
            _playerTransform = FindObjectOfType<PlayerController>().gameObject.transform;

        }

        private void Start()
        {
            StartAttacks();
        }

        public void StartAttacks()
        {
            _attackContainer = new GameObject();
            _attackContainer.name = "[EnemyAttacksContainer-RunModeOnly]";
            InvokeRepeating("InvokeAttack", timeBtwAttacks, timeBtwAttacks);
        }

        public void StopAttacks()
        {
            CancelInvoke();
        }


        

        //invoke the attacks
        private void InvokeAttack()
        {
            int rand = Random.Range(0, enemyAttacks.Length);
            EnemyAttackData attack = Instantiate<EnemyAttackData>(enemyAttacks[rand], _attackContainer.transform);

            //Fetch a random possible direction for the attack
            Direction attackDir = attack.GetRandomPossibleDirection();

            //fix position
            AttackTransformFixer.FixPosition(attack, attackDir,_playerTransform.position);

            AttackTransformFixer.FixRotation(attack, attackDir);
           
        }

       



    }
}