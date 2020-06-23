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

[CreateAssetMenu(fileName = "new Action", menuName = "Action")]
public class Action : ScriptableObject
{
    public string actionName;
    public ActionType actionType;
    public EffectType effectType;
    public float castVelocity;
    public int actionBarsNeeded;
    
    //jeito de triggar animação
}
