using System;
using System.Net.NetworkInformation;
using Squeak;
using UnityEngine;

namespace Scripts
{
    [CreateAssetMenu(fileName = "new Action", menuName = "Action Menu SO/Action/DamageElixirAttack")]
    public class DamageElixirAction : AttackAction
    {
        int _initialBaseDamage;

        public override int PhysicalDamage { get => base.PhysicalDamage * PlayerStatusController.damageCnt; }

        protected void OnEnable()
        {
            _initialBaseDamage = PhysicalDamage;
        }

        public override void DoAction(Animator anim, EnemyStatusController enemy, PlayerStatusController player,
            Animator playerAnim)
        {
            base.DoAction(anim, enemy, player, playerAnim);
            PlayerStatusController.damageCnt = 1;
        }
        

    }
}