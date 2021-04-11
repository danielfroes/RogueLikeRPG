using System.Collections.Generic;
using Squeak;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

[CreateAssetMenu(fileName = "new Action", menuName = "Action Menu SO/Action/LifeSteal")]

public class LifestealAction : AttackWithStatusAnimation
{
    [Range(0f, 100f)] [SerializeField] float _lifeStealPercentage;
    public override void DoAction(Animator attackAnim, EnemyStatusController enemy, PlayerStatusController player,
        Animator playerAnim)
    {
        base.DoAction(attackAnim, enemy, player, playerAnim);
        
        var healAmount = _lifeStealPercentage/100 * PhysicalDamage; 
        player.Heal(healAmount);
    }
}
