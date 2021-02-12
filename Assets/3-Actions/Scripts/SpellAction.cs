using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new SpellAction", menuName = "Action Menu SO/Action/Spell")]
public class SpellAction : Action
{
    public int BaseDamage;
    public override void DoAction(Animator anim)
    {
        base.DoAction(anim);
        Debug.Log("Spell");
    }
}
