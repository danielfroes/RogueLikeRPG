using Squeak;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "new Action", menuName = "Action Menu SO/Action/Attack")]
public class AttackAction : Action
{
    [FormerlySerializedAs("physicalDamage")] [SerializeField] int _physicalDamage;
    public virtual int PhysicalDamage => _physicalDamage;

    public override void DoAction(Animator anim, EnemyStatusController enemy, PlayerStatusController player,
        Animator playerAnim)
    {
        base.DoAction(anim, enemy, player, playerAnim);
        anim.transform.position = enemy.transform.position;
        enemy.Damage(PhysicalDamage);
    }
}
