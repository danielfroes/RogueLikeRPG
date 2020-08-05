using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Action", menuName = "Action/Attack")]
public class AttackAction : Action
{
    
    public int BaseDamage;


    public override void DoAction()
    {
        Debug.Log("Dando " + BaseDamage + " no inimigo");
    }     
    
}
