﻿using System.Collections;
using System.Collections.Generic;
using Squeak;
using UnityEngine;

[CreateAssetMenu(menuName = "Action Menu SO/Action/ComboAttack")]
public class ComboAction : AttackAction
{
    public float damageIncreasePercentage = 0f;
    public float cooldownReductionPercentage = 0f;
    
    public float damageMultiplier = 1f;
    public float castTimeMultiplier = 1f;
    
    public override void DoAction(Animator anim, EnemyStatusController enemy, PlayerStatusController player)
    {
        base.DoAction(anim, enemy, player);
        anim.transform.position = enemy.transform.position;
        enemy.Damage(physicalDamage * damageMultiplier);

        damageMultiplier += damageIncreasePercentage;
        castTimeMultiplier *= cooldownReductionPercentage;
    }
}
