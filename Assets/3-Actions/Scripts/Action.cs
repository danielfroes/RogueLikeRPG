    using System;
    using System.Collections;
using System.Collections.Generic;
    using Squeak;
    using UnityEngine;

//TODO: Tirar isso aqui
public enum ActionType {
    Attack,
    Spell,
    Item,
};

public abstract class Action : ScriptableObject {
    
    public string actionName;
    [TextArea(3, 10)] public string details;
    public ActionType actionType;
    public float castTime;
    public int actionBarsNeeded;
    public Sound activeSound;
    
    [SerializeField] AnimationClip _animationClip = null;
    AnimatorOverrideController _animatorController;
    
    static readonly int DoActionID = Animator.StringToHash("DoAction");

    public virtual void DoAction(Animator actionAnim, EnemyStatusController enemy , PlayerStatusController player )
    {
        if (_animatorController == null)
        {
            _animatorController = new AnimatorOverrideController(actionAnim.runtimeAnimatorController);
            var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>
            {
                new KeyValuePair<AnimationClip, AnimationClip>(_animatorController.animationClips[0],
                    _animationClip)
            };
            _animatorController.ApplyOverrides(anims);
        }

        actionAnim.runtimeAnimatorController = _animatorController;
            
        actionAnim.SetTrigger(DoActionID);
        
    }
    
    

}
