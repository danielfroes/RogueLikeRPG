using Squeak;
using UnityEngine;

[CreateAssetMenu(fileName = "new Action", menuName = "Action Menu SO/Action/Attack")]
public class AttackAction : Action {

    public int physicalDamage;
    public override void DoAction(Animator anim, EnemyStatusController enemy, PlayerStatusController player, bool gonnaCombo)
    {
        base.DoAction(anim, enemy, player, gonnaCombo);
        anim.transform.position = enemy.transform.position;
        if(physicalDamage != 0 || totalDmgOverTime != 0)
        {
            int DamageAmount = (int)(physicalDamage * player.get_player_attack());
            enemy.Damage(DamageAmount);
            int DamageOverTimeAmount = (int)(totalDmgOverTime * player.get_player_attack());
            enemy.DamageOverTime(DamageOverTimeAmount, timeOfDmgOverTime);
        }
        player.SkillStatusUpdate((int)statusType, statusAmount, statusDuration);
        if (gonnaCombo) { DoCombo(enemy, player); }

        player.ComboIncrement();
    }

}
