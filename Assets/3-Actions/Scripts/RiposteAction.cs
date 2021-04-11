using Squeak;
using UnityEngine;

[CreateAssetMenu(menuName = "Action Menu SO/Action/Riposte")]
public class RiposteAction : Action
{
    public float stunTime = 5f;
    public float activeTime = 2f;
    
    public override void DoAction(Animator actionAnim, EnemyStatusController enemy, PlayerStatusController player)
    {
        PlayerController.riposte = true;
        PlayerController.activateRiposteCoroutine = true;
        base.DoAction(actionAnim, enemy, player);
        actionAnim.transform.position = player.transform.position;
    }
}
