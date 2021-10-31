using Squeak;
using UnityEngine;

[CreateAssetMenu(fileName = "new PlayerAction", menuName = "PlayerAction Menu SO/PlayerAction/Attack")]
public class AttackAction : PlayerAction {

    public int physicalDamage;
    public override void DoAction(Animator anim, EnemyStatusController enemy, PlayerStatusController player, bool gonnaCombo)
    {
        base.DoAction(anim, enemy, player, gonnaCombo);
        if (animateOnPlayer) { anim.transform.position = player.transform.position; } else { anim.transform.position = enemy.transform.position; }
        //anim.transform.position = enemy.transform.position;
        if(physicalDamage != 0 || totalDmgOverTime != 0)
        {
            int DamageAmount = (int)(physicalDamage * player.GetPlayerAttack());
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
