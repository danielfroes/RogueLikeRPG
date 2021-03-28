using Squeak;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

[CreateAssetMenu(fileName = "new Action", menuName = "Action Menu SO/Action/LifeSteal")]

public class LifestealAction : AttackAction
{

    [Range(0f, 100f)] [SerializeField] float _lifeStealPercentage;
    [SerializeField] Animation _healAnimation;
    
    public override void DoAction(Animator anim, EnemyStatusController enemy, PlayerStatusController player)
    {
        base.DoAction(anim, enemy, player);
        var healAmount = _lifeStealPercentage/100 * PhysicalDamage;
  
        player.Heal(healAmount);

    }

}
