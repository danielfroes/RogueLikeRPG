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
    public ActionType actionType;
    public EffectType effectType; 
    public float castTime;
    public int actionBarsNeeded;
    public abstract void DoAction();
    
    //jeito de triggar animação
}
