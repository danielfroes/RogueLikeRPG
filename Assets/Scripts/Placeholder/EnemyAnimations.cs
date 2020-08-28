using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    
    [SerializeField] private Animator animator = null;

    private void Awake() {
    }


    public void ReceiveDamage()
    {
        animator.SetTrigger("ReceiveDamage");
    }
}
