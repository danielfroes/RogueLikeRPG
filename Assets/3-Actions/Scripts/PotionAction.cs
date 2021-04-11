using System;
using System.Collections.Generic;
using Squeak;
using UnityEngine;

[CreateAssetMenu(menuName = "Action Menu SO/Action/Potion")]
public class PotionAction : Action
{
    public static int stepCounter = 0;
    public const int NumOfSteps = 20;

    [SerializeField] AnimationClip _effectAnimation;
    AnimatorOverrideController _effectController = null;

    
    private void OnEnable()
    {
        stepCounter = 0;
    }

    public override void DoAction(Animator actionAnim, EnemyStatusController enemy, PlayerStatusController player,
        Animator playerAnim)
    {
        
        if (_effectController == null)
        {
            _effectController = new AnimatorOverrideController(playerAnim.runtimeAnimatorController);
            var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>
            {
                new KeyValuePair<AnimationClip, AnimationClip>(_effectController.animationClips[0],
                    _effectAnimation)
            };
            _effectController.ApplyOverrides(anims);
        }

        playerAnim.runtimeAnimatorController = _effectController;
        playerAnim.SetTrigger(DoActionID);
        
        
        
        stepCounter = 0;
        ActionBar.instance.numActions = 4;
        ActionBar.instance._isFull = true;
        ActionBar.instance.fillImg.fillAmount = 1;
        base.DoAction(actionAnim, enemy, player, playerAnim);
        actionAnim.transform.position = player.transform.position;
    }
}
