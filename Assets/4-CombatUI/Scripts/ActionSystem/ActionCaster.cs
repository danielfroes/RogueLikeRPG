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
    [SerializeField] EnemyStatusController _enemyStatusController = null;
    [SerializeField] PlayerStatusController _playerStatusController = null;
   
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void CastAction(Action action) {
        if (action is ComboAction combo)
        {
            castingBar.value = 0;
            castingBar.gameObject.SetActive(true);
            isCasting = true;

            float time = combo.castTime * combo.castTimeMultiplier;
            if (Mathf.Approximately(0f, time))
                time = 0f;
            
            castingBar.DOValue(castingBar.maxValue, time).OnComplete(() =>
            {
                action.DoAction(_actionAnim, _enemyStatusController, _playerStatusController);
                castingBar.gameObject.SetActive(false);
                isCasting = false;
            });
            
            return;
        }
        
        castingBar.value = 0;
        castingBar.gameObject.SetActive(true);
        isCasting = true;
        castingBar.DOValue(castingBar.maxValue, action.castTime).OnComplete(() =>
        {
            action.DoAction(_actionAnim, _enemyStatusController, _playerStatusController);
            castingBar.gameObject.SetActive(false);
            isCasting = false;
        });
    }

    public void CancelCasting() {
        isCasting = false;
        ActionMenuController.isMenuOpen = false;
        castingBar.gameObject.SetActive(false);
        castingBar.DOKill();
    }

}
