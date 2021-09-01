using System.Collections;
using System.Collections.Generic;
using Squeak;
using UnityEngine;

[CreateAssetMenu(fileName = "new SpellAction", menuName = "Action Menu SO/Action/Spell")]
public class SpellAction : Action
{
    public int magicalDamage;
    public override void DoAction(Animator anim, EnemyStatusController enemy, PlayerStatusController player, bool gonnaCombo)
    {
        base.DoAction(anim, enemy, player, gonnaCombo);
        anim.transform.position = enemy.transform.position;
        if (magicalDamage != 0 || totalDmgOverTime != 0)
        {
            int DamageAmount = (int)(magicalDamage * player.get_player_attack());
            enemy.Damage(DamageAmount);
            int DamageOverTimeAmount = (int)(totalDmgOverTime * player.get_player_attack());
            enemy.DamageOverTime(DamageOverTimeAmount, timeOfDmgOverTime);
        }
        player.SkillStatusUpdate((int)statusType, statusAmount, statusDuration);
        if (gonnaCombo) { DoCombo(enemy, player); }
        player.ComboIncrement();
    }
    
}

