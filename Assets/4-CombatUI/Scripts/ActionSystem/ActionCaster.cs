using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Squeak;

public class ActionCaster : MonoBehaviour {

    public bool isCasting = false;
    public static ActionCaster instance;
    [SerializeField] Slider castingBar = null;
    
    //TODO: Achar um jeito melhor de referenciar o enemyStatusController e o actionAnim(vai dar problema quando colocar mais de uma batalhat)
    [SerializeField] Animator _actionAnim = null;
    [SerializeField] Animator _playerEffectAnim = null;
    [SerializeField] EnemyStatusController _enemyStatusController = null;
    [SerializeField] PlayerStatusController _playerStatusController = null;
   

    public ActionMenuController controller;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void CastAction(Action action) {
        castingBar.value = 0;
        castingBar.gameObject.SetActive(true);
        isCasting = true;
        float time = action.castTime;

        if (action is ComboAction c)
            time *= c.castTimeMultiplier;
            
        
        castingBar.DOValue(castingBar.maxValue * 95/100, time).OnComplete(() =>
        {
            action.DoAction(_actionAnim, _enemyStatusController, _playerStatusController, _playerEffectAnim);
            castingBar.gameObject.SetActive(false);
            isCasting = false;
        });
    }

    public void CancelCasting()
    {
        isCasting = false;
        castingBar.gameObject.SetActive(false);
        castingBar.DOKill();
        controller.module.enabled = false;
    }

    void OnDestroy()
    {
        instance = null;
    }
}
