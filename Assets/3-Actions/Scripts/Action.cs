    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Tirar isso aqui
public enum ActionType {
    Attack,
    Spell,
    Item,
};

public enum EffectType {
    Damage,
    Heal,
}

public abstract class Action : ScriptableObject {
    
    public string actionName;
    [TextArea(3, 10)] public string details;
    public ActionType actionType;
    public EffectType effectType;
    public float castTime;
    public int actionBarsNeeded;

    [SerializeField] AnimationClip _animationClip;
    AnimatorOverrideController _animatorController;
    
    static readonly int DoActionID = Animator.StringToHash("DoAction");

    public virtual void DoAction(Animator anim)
    {
        if (_animatorController == null)
        {
            _animatorController = new AnimatorOverrideController(anim.runtimeAnimatorController);
            var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();
            anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(_animatorController.animationClips[0], _animationClip));
            _animatorController.ApplyOverrides(anims);
        }

        anim.runtimeAnimatorController = _animatorController;
            
        anim.SetTrigger(DoActionID);
        
    }
    
    
    
    //O animator do ataque ta dentro do player
    //Ele eh unico e compartilhado, entao a referencia dele pode ser pega de varias formas.
    //Posso colocoar ele no player e pegar a referencia do player
    //  O foda eh que eu precisaria fazer isso para cada acao sabe
    //Posso fazer um singleton sla
    //Eu posso colocar o animator base como propiedade do SO
    //  Dai eu crio o aoc dentro do scriptable object no OnEnable
    //  O problema eh que eu teria que criar ainda assim que passar a referencia do animator no DoAction para aplicar aoc
    //Da pra instanciar eu mesmo o prefab do attack animator
    // Mas dai eu precisaria de colocar no mesmo negocio vei horroroso

}
