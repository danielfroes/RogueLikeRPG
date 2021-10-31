using System.Collections;
using System.Collections.Generic;
using Squeak;
using UnityEngine;

[CreateAssetMenu(fileName = "new SpellAction", menuName = "PlayerAction Menu SO/PlayerAction/Spell")]
public class SpellAction : PlayerAction
{
    public int magicalDamage;
    public override void DoAction(Animator anim, EnemyStatusController enemy, PlayerStatusController player, bool gonnaCombo)
    {
        base.DoAction(anim, enemy, player, gonnaCombo);
        if (animateOnPlayer) { anim.transform.position = player.transform.position; } else { anim.transform.position = enemy.transform.position; }
        //anim.transform.position = enemy.transform.position;
        if (magicalDamage != 0 || totalDmgOverTime != 0)
        {
            int DamageAmount = (int)(magicalDamage * player.GetPlayerAttack());
            enemy.Damage(DamageAmount);
            int DamageOverTimeAmount = (int)(totalDmgOverTime * player.GetPlayerAttack());
            enemy.DamageOverTime(DamageOverTimeAmount, timeOfDmgOverTime);
        }
        player.SkillStatusUpdate((int)statusType, statusAmount, statusDuration);
        if (divineShield) { player.ActivateDivineShield(); }
        if (gonnaCombo) { DoCombo(anim, enemy, player, gonnaCombo); }
        player.ComboIncrement();
    }
    
}

