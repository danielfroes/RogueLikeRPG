using System.Collections;
using System.Collections.Generic;
using Squeak;
using UnityEngine;

[CreateAssetMenu(fileName = "new SpellAction", menuName = "PlayerAction Menu SO/PlayerAction/Spell")]
public class SpellAction : PlayerAction
{
    public int magicalDamage;
    public override void DoAction(Animator anim, EnemyStatusController enemy, PlayerStatusController player)
    {
        base.DoAction(anim, enemy, player);
        anim.transform.position = enemy.transform.position;
        enemy.Damage(magicalDamage);
        
    }
}
