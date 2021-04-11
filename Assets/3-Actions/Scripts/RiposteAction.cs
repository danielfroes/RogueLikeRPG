using Squeak;
using UnityEngine;

[CreateAssetMenu(menuName = "Action Menu SO/Action/Riposte")]
public class RiposteAction : AttackWithStatusAnimation
{
    public static float stunTime = 5f;
    public float time = 2f;
    
    public override void DoAction(Animator actionAnim, EnemyStatusController enemy, PlayerStatusController player,
        Animator playerAnim)
    {
        PlayerController.riposte = true;
        PlayerController.activateRiposteCoroutine = true;
        base.DoAction(actionAnim, enemy, player, playerAnim);
        actionAnim.transform.position = player.transform.position;
    }
}
