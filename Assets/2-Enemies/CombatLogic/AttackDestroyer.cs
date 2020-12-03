using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AttackDestroyer : MonoBehaviour
{
    public void DestroyAttack()
    {
        Destroy(this.gameObject);
    }
}
