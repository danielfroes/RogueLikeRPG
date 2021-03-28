using System.Runtime.Remoting.Messaging;
using Squeak;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "new Action", menuName = "Action Menu SO/Action/Attack")]
public class AttackAction : Action
{


    [FormerlySerializedAs("physicalDamage")] [SerializeField] int _physicalDamage;
    public virtual int PhysicalDamage => _physicalDamage;

    public override void DoAction(Animator anim, EnemyStatusController enemy, PlayerStatusController player)
    {
        base.DoAction(anim, enemy, player);
        anim.transform.position = enemy.transform.position;
        enemy.Damage(PhysicalDamage);
        
    }

}
