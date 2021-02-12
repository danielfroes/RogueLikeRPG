using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Action", menuName = "Action Menu SO/Action/Attack")]
public class AttackAction : Action {

    public int BaseDamage;
    public override void DoAction(Animator anim)
    {
        base.DoAction(anim);
        Debug.Log("Tomou Dano");

    }

}
