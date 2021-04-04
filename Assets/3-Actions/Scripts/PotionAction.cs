using Squeak;
using UnityEngine;

[CreateAssetMenu(menuName = "Action Menu SO/Action/Potion")]
public class PotionAction : Action
{
    public static int stepCounter = 0;
    public const int NumOfSteps = 20;
    
    public override void DoAction(Animator actionAnim, EnemyStatusController enemy, PlayerStatusController player)
    {
        stepCounter = 0;
        ActionBar.instance.numActions = 4;
        ActionBar.instance._isFull = true;
        ActionBar.instance.fillImg.fillAmount = 1;
        base.DoAction(actionAnim, enemy, player);
        actionAnim.transform.position = player.transform.position;
    }
}
