using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor;
using UnityEditorInternal;


public class ActionCaster : MonoBehaviour {

    public bool isCasting = false;
    public static ActionCaster instance;
    [SerializeField] private Slider castingBar = null;


    //**Tirar isso depois do teste
    [SerializeField] private Animator attackAnim = null;

    private void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        
    }



    public void CastAction(Action action) {
        castingBar.value = 0;
        castingBar.gameObject.SetActive(true);
        isCasting = true;
        castingBar.DOValue(castingBar.maxValue, action.castTime).OnComplete(() =>
        {

            //TODO: Gambiarra
           
            action.DoAction(attackAnim);
            
            //TODO: Gambiarra
            
            castingBar.gameObject.SetActive(false);
            isCasting = false;
        });
    }

    public void CancelCasting() {
        isCasting = false;
        castingBar.gameObject.SetActive(false);
        castingBar.DOKill();
    }

}
