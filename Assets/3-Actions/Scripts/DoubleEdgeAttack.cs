using Squeak;
using UnityEngine;

[CreateAssetMenu(fileName = "new Action", menuName = "Action Menu SO/Action/DoubleEdgeAttack")]
public class DoubleEdgeAttack : AttackAction {

    [Range(0f, 100f)] [SerializeField] float _playerDamageLifePercentage;
    [SerializeField] Animation _playerDamageAnimation;
    
    public override void DoAction(Animator anim, EnemyStatusController enemy, PlayerStatusController player,
        Animator playerAnim)
    {
        base.DoAction(anim, enemy, player, playerAnim);

        var damageAmount = player.preset.maxHealth * _playerDamageLifePercentage / 100;
        
        player.TrueDamage(damageAmount);
    }
}
