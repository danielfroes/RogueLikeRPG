using Squeak;
using UnityEngine;

[CreateAssetMenu(fileName = "new Action", menuName = "Action Menu SO/Action/Attack")]
public class AttackAction : Action {

    public int physicalDamage;
    public override void DoAction(Animator anim, EnemyStatusController enemy, PlayerStatusController player)
    {
        base.DoAction(anim, enemy, player);
        anim.transform.position = enemy.transform.position;
        enemy.Damage(physicalDamage);
        
    }

}
