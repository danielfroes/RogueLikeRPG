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
                     EnemyStatusController _enemyStatusController = null;
    [SerializeField] PlayerStatusController _playerStatusController = null;
    [SerializeField] ActionTargetSelector _actionTargetSelector = null;
   
    //[SerializeField] EnemySelector _enemySelector = null;
   
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void CastAction(PlayerAction action, bool gonnaCombo) {
        castingBar.value = 0;
        castingBar.gameObject.SetActive(true);
        isCasting = true;
        castingBar.DOValue(castingBar.maxValue, (float)(action.castTime / _playerStatusController.GetCastVelocity())).OnComplete(() =>
        {
            //_enemyStatusController = _enemySelector.selectedEnemy.GetComponent<EnemyStatusController>();
            _enemyStatusController = _actionTargetSelector.SelectedEnemy;
            action.DoAction(_actionAnim, _enemyStatusController, _playerStatusController, gonnaCombo);
            castingBar.gameObject.SetActive(false);
            isCasting = false;
        });
    }

    public void CancelCasting() {
        isCasting = false;
        castingBar.gameObject.SetActive(false);
        castingBar.DOKill();
    }

    public bool GonnaCombo()
    {
        return (_playerStatusController.combo > 0);
    }

}
