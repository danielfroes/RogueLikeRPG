using Squeak;
using UnityEngine;

[CreateAssetMenu(fileName = "new PlayerAction", menuName = "PlayerAction Menu SO/PlayerAction/Attack")]
public class AttackAction : PlayerAction {

    public int physicalDamage;
    public override void DoAction(Animator anim, EnemyStatusController enemy, PlayerStatusController player)
    {
        base.DoAction(anim, enemy, player);
        anim.transform.position = enemy.transform.position;
        enemy.Damage(physicalDamage);
        
    }

}
