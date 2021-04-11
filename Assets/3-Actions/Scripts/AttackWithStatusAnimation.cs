using System.Collections.Generic;
using Squeak;
using UnityEngine;

public class AttackWithStatusAnimation: AttackAction
{
    [SerializeField] AnimationClip _effectAnimation;
    AnimatorOverrideController _effectController = null;
    public override void DoAction(Animator attackAnim, EnemyStatusController enemy, PlayerStatusController player,
        Animator playerAnim)
    {
        base.DoAction(attackAnim, enemy, player, playerAnim);

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

    }
}