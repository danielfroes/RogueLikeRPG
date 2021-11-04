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

public enum StatusUpdateType
{
    None,
    Attack,
    Defense,
    Velocity,
    CastSpeed,
    StaminaChargeSpeed,
    Hp,
}



public abstract class PlayerAction : ScriptableObject {
    
    public string actionName;
    [TextArea(3, 10)] public string details;
    public ActionType actionType;
    public float castTime;
    public int actionBarsNeeded;
    public Sound activeSound;
    public float totalDmgOverTime;
    public float timeOfDmgOverTime;
    public StatusUpdateType statusType;
    public float statusAmount;
    public float statusDuration;
    public bool divineShield;
    public int comboDamage;
    public PlayerAction combo;
    public bool animateOnPlayer;
    public bool onCountdownEnd;
    
    [SerializeField] AnimationClip _animationClip = null;
    AnimatorOverrideController _animatorController;
    
    static readonly int DoActionID = Animator.StringToHash("DoAction");

    public virtual void DoAction(Animator actionAnim, EnemyStatusController enemy , PlayerStatusController player, bool gonnaCombo)
    {
        AnimationClip _animToPlay = _animationClip;
        if (_animatorController == null)
        {
            _animatorController = new AnimatorOverrideController(actionAnim.runtimeAnimatorController);
            var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>
            {
                new KeyValuePair<AnimationClip, AnimationClip>(_animatorController.animationClips[0],
                    _animToPlay)
            };
            _animatorController.ApplyOverrides(anims);
        }

        actionAnim.runtimeAnimatorController = _animatorController;
            
        actionAnim.SetTrigger(DoActionID);
        
    }
    
    public void DoCombo(Animator anim, EnemyStatusController enemy, PlayerStatusController player, bool gonnaCombo)
    {
        if (comboDamage != 0)
        {
            int DamageAmount = (int)(comboDamage * player.GetPlayerAttack());
            enemy.Damage(DamageAmount);
        }
        if (combo != null)
        {
            combo.DoAction(anim, enemy, player, gonnaCombo);
        }
    }

    //O animator do ataque ta dentro do player
    //Ele eh unico e compartilhado, entao a referencia dele pode ser pega de várias formas.
    //Posso colocoar ele no player e pegar a referencia do player
    //  O foda eh que eu precisaria fazer isso para cada acao sabe
    //Posso fazer um singleton sla
    //Eu posso colocar o animator base como propiedade do SO
    //  Dai eu crio o aoc dentro do scriptable object no OnEnable
    //  O problema eh que eu teria que criar ainda assim que passar a referencia do animator no DoAction para aplicar aoc
    //Da pra instanciar eu mesmo o prefab do attack animator
    // Mas dai eu precisaria de colocar no mesmo negocio vei horroroso

}
