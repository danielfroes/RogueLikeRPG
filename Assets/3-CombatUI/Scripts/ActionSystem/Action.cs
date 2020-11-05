using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ActionType {
    attack,
    spell,
    item
};

public enum EffectType {
    damage,
    heal
}


public abstract class Action: ScriptableObject
{
    public string actionName;

    [TextArea(3,10)]
    public string details;
    public ActionType actionType;
    public EffectType effectType; 
    public float castTime;
    public int actionBarsNeeded;
    public abstract void DoAction(Animator anim);
    
    //jeito de triggar animação
}
